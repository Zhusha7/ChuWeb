using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuWeb.API.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public int PostId { get; set; }
        
        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        
        public int? ParentCommentId { get; set; }
        
        [ForeignKey("ParentCommentId")]
        public Comment? ParentComment { get; set; }
        
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        
        public bool IsApproved { get; set; } = false;
    }
} 