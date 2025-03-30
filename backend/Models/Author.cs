using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChuWeb.API.Models
{
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Bio { get; set; }
        
        [StringLength(200)]
        public string? ProfilePictureUrl { get; set; }
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Username { get; set; }
        
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
} 