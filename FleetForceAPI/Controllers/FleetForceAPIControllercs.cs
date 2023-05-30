using FleetForceAPI.Data;
using FleetForceAPI.Models;
using FleetForceAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FleetForceAPI.Controllers
{
    [Route("api/FleetForceAPI")]
    [ApiController]
    public class FleetForceAPIControllercs : ControllerBase 
    {
        [HttpGet]
        public IEnumerable<FleetDTO> GetFleets() 
        {
            return FleetStore.fleetList;
        }

        [HttpGet("id")]
        public FleetDTO GetFleets(int id)
        {
            return FleetStore.fleetList.FirstOrDefault(u=>u.Id==id);
        }
    }
}
