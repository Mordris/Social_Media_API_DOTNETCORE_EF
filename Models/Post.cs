using System;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_API.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; } // Foreign key for the user who created the post
        public User User { get; set; } // Navigation property to access the user who created the post

        // Add any other properties you need for the post entity
    }
}
