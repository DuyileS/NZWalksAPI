using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of three characters")]
        [MaxLength(3, ErrorMessage = "Code cannot be more than three characters")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
