using ENERGY_NOW_BE.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENERGY_NOW_BE.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet("user-only")]
        [Authorize(Policy = "ClientAccess")]
        public IActionResult GetUserOnlyData()
        {
            return Ok(new { message = "This is user-only data." });
        }

    }
}
