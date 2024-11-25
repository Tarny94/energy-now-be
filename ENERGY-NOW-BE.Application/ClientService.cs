using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using ENERGY_NOW_BE.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Application
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

        public async Task<string> ClientConfiguration(ClientRequest client)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(client.UserId);

                if (user == null) return "User not exist!!!";
                Client clientCheck = await _clientConfigurationRepository.GetClientConfigurationByIdAsync(client.UserId);

                if (clientCheck != null && clientCheck.UserId == client.UserId) return "Client already configured";

                var clientConfiguration = CreateClientForDB(client);

                await _clientConfigurationRepository.AddClientConfigurationAsync(clientConfiguration);

                Client result = await _clientConfigurationRepository.GetClientConfigurationByIdAsync(client.UserId);

                await _clientConfigurationRepository.AddSpecializationsToClient(result.Id, client.Specialization);

                return "Client configuration was successful";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Client CreateClientForDB(ClientRequest clientConfigurationRequest)
        {
            return new Client()
            {
                Icon  = clientConfigurationRequest.Icon,
                Cui = clientConfigurationRequest.Cui,
                City = clientConfigurationRequest.City,
                ClientName = clientConfigurationRequest.ClientName,
                Email = clientConfigurationRequest.Email,
                County = clientConfigurationRequest.County,
                Phone = clientConfigurationRequest.Phone,
                Details = clientConfigurationRequest.Details,
                IsAuthorizated = false,
                IsConfirmed = false,
                UserId = clientConfigurationRequest.UserId,
            };
        }
    }
}
