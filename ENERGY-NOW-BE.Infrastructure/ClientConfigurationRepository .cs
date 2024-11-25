using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Enum;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.Extensions.Logging.Abstractions;
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

        public Task<Client> GetClientConfigurationByIdAsync(string id)
        {
            var result = _context.ClientConfigurations
                                 .FirstOrDefault(c => c.Id.ToString() == id);
            return Task.FromResult(result);
        }

        public Task<List<Client>> GetAllClientConfigurationsAsync()
        {
            List<Client> result = _context.ClientConfigurations.ToList();
            return Task.FromResult(result);
        }

        public async Task AddSpecializationsToClient(Guid clientId, List<Specialization> specializations)
        {
            var client = _context.ClientConfigurations.Include(c => c.ClientSpecializations)
                                                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
                throw new Exception("Client not found");

            foreach (var specialization in specializations)
            {
                if (!client.ClientSpecializations.Any(cs => cs.Specialization == specialization))
                {
                    client.ClientSpecializations.Add(new ClientSpecialization
                    {
                        ClientId = clientId,
                        Specialization = specialization
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Client>> GetClientsBySpecializations(List<Specialization> specializations)
        {
            return await _context.ClientConfigurations
                                 .Include(c => c.ClientSpecializations)
                                 .Where(c => c.ClientSpecializations
                                              .Any(cs => specializations.Contains(cs.Specialization)))
                                 .ToListAsync();
        }
        public async Task<List<Specialization>> GetSpecializationsForClient(Guid clientId)
        {
            // Query the ClientSpecializations table to filter by ClientId
            var specializations = _context.ClientSpecializations
                                                .Where(cs => cs.ClientId == clientId)
                                                .Select(cs => cs.Specialization)
                                                .ToList();

            if (!specializations.Any())
                return new List<Specialization>();

            return specializations;
        }






        public Task<List<Client>> GetAllFilteredClients(string County, string City)
        {
            //List<ClientConfiguration> result = _context.ClientConfigurations
            //    .Where(e => e.County == County)
            //    .Where(e => e.City == City)
            //    .Where(e => e.Specialization.Any(s => specializations)
            //    .ToList();
            return null;
        }

        public async Task AddClientConfigurationAsync(Client clientConfiguration)
        {
            _context.ClientConfigurations.Add(clientConfiguration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientConfigurationAsync(Client clientConfiguration)
        {
            _context.ClientConfigurations.Update(clientConfiguration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientConfigurationAsync(string id)
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
