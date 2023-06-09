using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        // Navigation property to access the associated Profile
        public Profile Profile { get; set; }

        // Navigation property to access the user's posts
        public ICollection<Post> Posts { get; set; }

        // Add any other properties or relationships you need for the user entity
    }
}
