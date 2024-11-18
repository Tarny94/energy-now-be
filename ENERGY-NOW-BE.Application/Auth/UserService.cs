using ENERGY_NOW_BE.Core;
using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using ENERGY_NOW_BE.Infrastructure;
using Microsoft.AspNetCore.Identity;


namespace ENERGY_NOW_BE.Application.Auth
{
    public class UserService : IUserService
    { 

        private readonly UserManager<User> _userManager;

        private readonly ClientConfigurationRepository _clientConfigurationRepository;

        public UserService(UserManager<User> userManager, ClientConfigurationRepository clientConfigurationRepository)
        {
            _userManager = userManager;
            _clientConfigurationRepository = clientConfigurationRepository;
        }

    }

}
