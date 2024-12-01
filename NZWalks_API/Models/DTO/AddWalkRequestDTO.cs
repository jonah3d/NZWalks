using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(0, 250)]
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }
        [Required]  
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

    }
}
