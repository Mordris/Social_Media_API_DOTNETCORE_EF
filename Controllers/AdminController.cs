using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Social_Media_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Only users with the "Admin" role can access these actions
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAdminData()
        {
            // This action is only accessible to users with the "Admin" role
            // Implement your logic here

            return Ok("Admin data");
        }

        [HttpPost]
        public IActionResult CreateAdminUser([FromBody] AdminUserModel user)
        {
            // This action is only accessible to users with the "Admin" role
            // Implement your logic here to create an admin user
            // The user parameter is received from the request body

            // Example: Create admin user code
            // var newUser = new User
            // {
            //     Username = user.Username,
            //     Password = user.Password,
            //     Email = user.Email,
            //     FullName = user.FullName,
            //     Role = "Admin"
            // };
            // // Save the new user to the database

            return Ok("Admin user created");
        }
    }

    public class AdminUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
