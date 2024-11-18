using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using ENERGY_NOW_BE.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Application.Auth
{
    public class ClientService : IClientService
    {
        private readonly ClientConfigurationRepository _clientConfigurationRepository;
        private readonly UserManager<User> _userManager;

        public ClientService(ClientConfigurationRepository clientConfigurationRepository, UserManager<User> userManager)
        {
            _clientConfigurationRepository = clientConfigurationRepository;
            _userManager = userManager;
        }

        public async Task<string> ClientConfiguration(ClientConfiguration clientConfiguration)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(clientConfiguration.UserId);

                if (user == null) return "User not exist!!!";

                await _clientConfigurationRepository.AddClientConfigurationAsync(clientConfiguration);

                return "Client configuration was successful";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ClientConfirmation(string ClientId)
        {
            //var client = await _clientConfigurationRepository.GetClientConfigurationByIdAsync
            return null;
        }
    }
}
