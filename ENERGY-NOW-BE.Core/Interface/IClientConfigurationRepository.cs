using ENERGY_NOW_BE.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Interface
{
    public interface IClientConfigurationRepository
    {
        Task<ClientConfiguration> GetClientConfigurationByIdAsync(int id);
        Task<IEnumerable<ClientConfiguration>> GetAllClientConfigurationsAsync();
        Task AddClientConfigurationAsync(ClientConfiguration clientConfiguration);
        Task UpdateClientConfigurationAsync(ClientConfiguration clientConfiguration);
        Task DeleteClientConfigurationAsync(int id);
    }
}
