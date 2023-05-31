using FleetForceAPI.Data;
using FleetForceAPI.Models;
using FleetForceAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FleetForceAPI.Controllers
{
    [Route("api/FleetForceAPI")]
    [ApiController]
    public class FleetForceAPIControllercs : ControllerBase 
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FleetDTO>> GetFleets() 
        {
            return Ok(FleetStore.fleetList);
        }

        [HttpGet("{id:int}", Name = "GetFleet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FleetDTO> GetFleets(int id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }

            var fleets = FleetStore.fleetList.FirstOrDefault(u => u.Id == id);

            if(fleets == null)
            {
                return NotFound();
            }

            return Ok(fleets); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FleetDTO> CreateFleet([FromBody]FleetDTO fleetDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if(fleetDTO == null)
            {
                return BadRequest(fleetDTO);
            }

            if(fleetDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            fleetDTO.Id = FleetStore.fleetList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            FleetStore.fleetList.Add(fleetDTO);

            return CreatedAtRoute("GetFleet", new { id = fleetDTO.Id }, fleetDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteFleet")]
        public IActionResult DeleteFleet(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var fleet = FleetStore.fleetList.FirstOrDefault(u => u.Id == id);

            if(fleet == null)
            {
                return NotFound();
            }

            FleetStore.fleetList.Remove(fleet);

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateFleet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFleet(int id, [FromBody]FleetDTO fleetDTO) 
        {
            if(fleetDTO == null || id != fleetDTO.Id)
            {
                return BadRequest();
            }
            
            var fleet = FleetStore.fleetList.FirstOrDefault(u => u.Id == id);
            fleet.Name = fleetDTO.Name;
            fleet.LoadSize = fleetDTO.LoadSize;
            fleet.FleetType = fleetDTO.FleetType;

            return NoContent();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialFleet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialFleet(int id, JsonPatchDocument<FleetDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var fleet = FleetStore.fleetList.FirstOrDefault(u => u.Id == id);
            if (fleet == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(fleet, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
