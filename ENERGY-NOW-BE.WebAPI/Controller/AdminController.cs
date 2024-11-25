using ENERGY_NOW_BE.Application;
using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENERGY_NOW_BE.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController
    {
        private readonly IAdminService _adminService;
        public AdminController( IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("client")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<List<AdminClientsListResponse>> GetClientList()
        {
            return await _adminService.GetAdminClientList();
        }

        [HttpPost("client/confirmation")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<string> ClientConfirmation([FromBody] ConfirmClient confirmClient)
        {
            return await _adminService.ClientConfirmation(confirmClient);
        }
    }
}
