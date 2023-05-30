using FleetForceAPI.Models.Dto;

namespace FleetForceAPI.Data
{
    public static class FleetStore
    {
        public static List<FleetDTO> fleetList = new List<FleetDTO>{
                new FleetDTO{Id=1, Name="Fleet 1"},
                new FleetDTO{Id=2, Name="Fleet 2"}
            };
    }
}
