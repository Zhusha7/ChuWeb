using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuWeb.API.Models
{
    public class Post : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Summary { get; set; }
        
        [StringLength(200)]
        public string? FeaturedImageUrl { get; set; }
        
        public bool IsPublished { get; set; } = false;
        
        public DateTime? PublishedAt { get; set; }
        
        [StringLength(200)]
        public string Slug { get; set; } = string.Empty;
        
        public int? AuthorId { get; set; }
        
        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
        
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
} 