using ENERGY_NOW_BE.Application.Auth;
using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Entity.Auth;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENERGY_NOW_BE.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user-only")]
        [Authorize(Policy = "UserAccess")]
        public IActionResult GetUserOnlyData()
        {
            return Ok(new { message = "This is user-only data." });
        }

        [HttpPost("configuration")]
        [Authorize(Policy = "ClientAccess")]
        public async Task<string> ClientConfiguration([FromBody] ClientConfiguration model)
        {
            return await _userService.ClientConfiguration(model);
        }
    }
}
