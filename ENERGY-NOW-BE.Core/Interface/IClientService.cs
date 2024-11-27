using ENERGY_NOW_BE.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Interface
{
    public interface IClientService
    {
        Task<string> ClientConfiguration(ClientRequest clientConfiguration);

        Task<List<GetFilteredClientResponse>> GetFilteredClients(GetFilteredClientRequest filteredClient); 

    }
}
