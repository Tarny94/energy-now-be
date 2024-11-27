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
        public Guid Id { get; set; }
        public string Icon { get; set; } //TODO
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public int Review { get; set; }
        public string Phone{ get; set; }     
        public List<Specialization> Specializations { get; set; } = new List<Specialization>();
        public bool Authorize { get; set; } 
        public string Description { get; set; } //TODO Create a tamplate configuration for Clients
    }
}
