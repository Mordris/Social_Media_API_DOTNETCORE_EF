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
            return Ok("Admin data");
        }

        [HttpPost]
        public IActionResult CreateAdminUser([FromBody] AdminUserModel user)
        {
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
