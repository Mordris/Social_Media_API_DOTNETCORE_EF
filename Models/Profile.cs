using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Social_Media_API.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public string PhotoUrl { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }

        // Navigation property to access the associated User
        public User User { get; set; }
    }
}
