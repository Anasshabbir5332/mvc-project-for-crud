using System.ComponentModel.DataAnnotations;

namespace mvc_project_for_crud.Models
{
    public class ProductDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
        [Required, MaxLength(100)]
        public string Brand { get; set; } = "";
        [Required, MaxLength(100)]
        public string Category { get; set; } = "";
        [Required]
        public string Price { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        public IFormFile? Imagefile { get; set; }

    }
}
