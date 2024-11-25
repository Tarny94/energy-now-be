using ENERGY_NOW_BE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class GetFilteredClientResponse
    {
        public string Icon { get; set; } //TODO
        public string ClientName { get; set; }
        public string Email { get; set; }
        public int Review { get; set; }
        public string Phone{ get; set; }     
        public List<ClientSpecialization> Specializations { get; set; } = new List<ClientSpecialization>();
        public string Authorize { get; set; } 
        public string Description { get; set; } //TODO Create a tamplate configuration for Clients
    }
}
