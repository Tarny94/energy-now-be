using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENERGY_NOW_BE.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController( IClientService clientService ) 
        {
           _clientService = clientService;
        }

        [HttpPost("configuration")]
        [Authorize(Policy = "UserAccess")]
        public async Task<string> ClientConfiguration([FromBody] ClientConfiguration model)
        {
            return await _clientService.ClientConfiguration(model);
        }
    }
}
