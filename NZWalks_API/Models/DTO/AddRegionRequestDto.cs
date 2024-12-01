using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a Maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name has to be a Maximum of 50 characters")]
        public string Name { get; set; }

        public string? RegionImageurl { get; set; }
    }
}
