using System.ComponentModel.DataAnnotations;

namespace FleetForceAPI.Models.Dto
{
    public class FleetDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int LoadSize { get; set; }
        public int FleetType { get; set; }
    }
}
