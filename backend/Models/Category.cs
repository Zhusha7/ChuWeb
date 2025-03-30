using System.ComponentModel.DataAnnotations;

namespace ChuWeb.API.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(100)]
        public string Slug { get; set; } = string.Empty;
        
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
} 