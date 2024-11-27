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

                Client clientCheck = await _clientConfigurationRepository.GetClientConfigurationByUserIdAsync(user.Id);

                if (clientCheck != null && clientCheck.UserId == client.UserId) return "Client already configured";

                var clientConfiguration = CreateClientForDB(client);

                await _clientConfigurationRepository.AddClientConfigurationAsync(clientConfiguration);

                Client result = await _clientConfigurationRepository.GetClientConfigurationByUserIdAsync(client.UserId);

                await _clientConfigurationRepository.AddSpecializationsToClient(result.Id, client.Specialization);

                return "Client configuration was successful";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetFilteredClientResponse>> GetFilteredClients(GetFilteredClientRequest filteredClient)
        {
            List<GetFilteredClientResponse> clientResponses = new List<GetFilteredClientResponse>();

            if (filteredClient.County != "" && filteredClient.City == "" && filteredClient.Specializations.Count == 0)
            {
                var result = await _clientConfigurationRepository.GetClientsFilteredByCounty(filteredClient.County);

                if (result == null) throw new Exception("Something went Wrong with county Filter");

                var responseTask = result.Select(async client => new GetFilteredClientResponse
                {
                    Id = client.Id,
                    Icon = client.Icon,
                    ClientName = client.ClientName,
                    Email = client.Email,
                    County = client.County,
                    City = client.City,
                    Review = 0, //TODO
                    Phone = client.Phone,
                    Specializations = await _clientConfigurationRepository.GetSpecializationsForClient(client.Id),
                    Authorize = client.IsAuthorizated,
                    Description = client.Details
                });

                var responseList = await Task.WhenAll(responseTask);

                return responseList.ToList();
            }

            if (filteredClient.County != "" && filteredClient.City != "" && filteredClient.Specializations.Count == 0)
            {
                var result = await _clientConfigurationRepository.GetClientsFilteredByCountyAndCity(filteredClient.County, filteredClient.City);

                if (result == null) throw new Exception("Something went Wrong with county Filter");

                var responseTask = result.Select(async client => new GetFilteredClientResponse
                {
                    Id = client.Id,
                    Icon = client.Icon,
                    ClientName = client.ClientName,
                    Email = client.Email,
                    County = client.County,
                    City = client.City,
                    Review = 0, //TODO
                    Phone = client.Phone,
                    Specializations = await _clientConfigurationRepository.GetSpecializationsForClient(client.Id),
                    Authorize = client.IsAuthorizated,
                    Description = client.Details
                });

                var responseList = await Task.WhenAll(responseTask);

                return responseList.ToList();
            }

            if (filteredClient.County == "" && filteredClient.City == "" && filteredClient.Specializations.Count != 0)
            {
                var result = await _clientConfigurationRepository.GetClientsBySpecializations(filteredClient.Specializations);

                if (result == null) throw new Exception("Something went Wrong with county Filter");

                var responseTask = result.Select(async client => !client.IsConfirmed ? null : new GetFilteredClientResponse
                {
                    Id = client.Id,
                    Icon = client.Icon,
                    ClientName = client.ClientName,
                    Email = client.Email,
                    County = client.County,
                    City = client.City,
                    Review = 0, //TODO
                    Phone = client.Phone,
                    Specializations = await _clientConfigurationRepository.GetSpecializationsForClient(client.Id),
                    Authorize = client.IsAuthorizated,
                    Description = client.Details
                });

                var responseList = await Task.WhenAll(responseTask);

                return responseList.ToList();
            }

            return null;
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
