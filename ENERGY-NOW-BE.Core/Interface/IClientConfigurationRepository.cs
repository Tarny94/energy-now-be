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
        Task AddClientConfigurationAsync(Client clientConfiguration);
        Task AddSpecializationsToClient(Guid clientId, List<Specialization> specializations);
        Task<List<Client>> GetClientsBySpecializations(List<Specialization> specializations);
        Task<List<Specialization>> GetSpecializationsForClient(Guid clientId);
        Task UpdateClientConfigurationAsync(Client clientConfiguration);
        Task DeleteClientConfigurationAsync(string id);
    }
}
