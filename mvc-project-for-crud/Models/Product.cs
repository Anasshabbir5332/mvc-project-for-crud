using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace mvc_project_for_crud.Models
{
    public class Product
    {
        [MaxLength(100)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        [Precision(16, 2)]
        public string Price { get; set; } = string.Empty;
        [MaxLength(400)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(100)]
        public string ImageFilename { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }




    }
}
