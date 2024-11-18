using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Infrastructure
{
    public class ClientConfigurationRepository : IClientConfigurationRepository
    {
        private readonly DataContext _context;

        public ClientConfigurationRepository(DataContext context)
        {
            _context = context;
        }

        //public async Task<ClientConfiguration> GetClientConfigurationByIdAsync(int id)
        //{
        //    return await _context.ClientConfigurations
        //                         .FirstOrDefaultAsync(c => c.Id == id);
        //}

        public async Task<IEnumerable<ClientConfiguration>> GetAllClientConfigurationsAsync()
        {
            return await _context.ClientConfigurations
                                 .ToListAsync();
        }

        public async Task AddClientConfigurationAsync(ClientConfiguration clientConfiguration)
        {
            _context.ClientConfigurations.Add(clientConfiguration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientConfigurationAsync(ClientConfiguration clientConfiguration)
        {
            _context.ClientConfigurations.Update(clientConfiguration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientConfigurationAsync(int id)
        {
            var clientConfiguration = await _context.ClientConfigurations.FindAsync(id);
            if (clientConfiguration != null)
            {
                _context.ClientConfigurations.Remove(clientConfiguration);
                await _context.SaveChangesAsync();
            }
        }
    }
}
