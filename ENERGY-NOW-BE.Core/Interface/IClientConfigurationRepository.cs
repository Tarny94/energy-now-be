using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Interface
{
    public interface IClientConfigurationRepository
    {
        Task<Client> GetClientConfigurationByIdAsync(string id);
        Task<List<Client>> GetAllClientConfigurationsAsync();
        Task<Client> GetClientConfigurationByUserIdAsync(string id);
        Task AddClientConfigurationAsync(Client clientConfiguration);
        Task AddSpecializationsToClient(Guid clientId, List<Specialization> specializations);
        Task<List<Client>> GetClientsBySpecializations(List<Specialization> specializations);
        Task<List<Specialization>> GetSpecializationsForClient(Guid clientId);
        Task UpdateClientConfigurationAsync(Client clientConfiguration);
        Task DeleteClientConfigurationAsync(string id);
        Task<List<Client>> GetClientsFilteredByCounty(string county);
        Task<List<Client>> GetClientsFilteredByCountyAndCity(string county, string city);
        Task<List<Client>> GetClientsFilteredByCountyCityAndSpecialization(string county, string city, List<Specialization> specializations);
    }
}
