using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Enum;
using ENERGY_NOW_BE.Core.Interface;
using ENERGY_NOW_BE.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Application
{
    public class AdminService : IAdminService
    {

        private readonly ClientConfigurationRepository _clientConfigurationRepository;
        private readonly UserManager<User> _userManager;

        public AdminService(ClientConfigurationRepository clientConfigurationRepository, UserManager<User> userManager)
        {
            _clientConfigurationRepository = clientConfigurationRepository;
            _userManager = userManager;
        }

        public async Task<string> ClientConfirmation(ConfirmClient confirmClient)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(confirmClient.UserId);
                Client client = await _clientConfigurationRepository.GetClientConfigurationByIdAsync(confirmClient.ClientId);

                if (user == null || client == null) return "User / Client not exist!!!";

                user.IsAClient = confirmClient.IsClientConfirmed;
                client.IsAuthorizated = confirmClient.IsAuthorizated;
                client.IsConfirmed = confirmClient.IsClientConfirmed;

                await _userManager.UpdateAsync(user);
                await _clientConfigurationRepository.UpdateClientConfigurationAsync(client);

                return "Client successful modified ";
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<List<AdminClientsListResponse>> GetAdminClientList()
        {
            var clientListDB = await _clientConfigurationRepository.GetAllClientConfigurationsAsync();

            if (clientListDB == null) throw new Exception("Something went wrong");
            //if (clientListDB != null && clientListDB.Count == 0) throw new Exception("No client found");

            var adminClientTasks = clientListDB.Select(async client => new AdminClientsListResponse
            {
                ClientId = client.Id,
                UserId = client.UserId,
                Cui = client.Cui,
                Phone = client.Phone,
                ClientName = client.ClientName,
                Email = client.Email,
                IsAuthorize = client.IsAuthorizated,
                IsConfirmed = client.IsConfirmed,
                Specialization = await GetSpecializationsForEachCLient(client.Id),

            });

            var adminClientList = await Task.WhenAll(adminClientTasks);

            return adminClientList.ToList();
        }

        private async Task<List<Specialization>> GetSpecializationsForEachCLient (Guid clientId)
        {
            var Specializations = await _clientConfigurationRepository.GetSpecializationsForClient(clientId);
            if (Specializations == null) throw new Exception("Something went wrong with specialization fetching");
            return Specializations;
        }
    }
}
