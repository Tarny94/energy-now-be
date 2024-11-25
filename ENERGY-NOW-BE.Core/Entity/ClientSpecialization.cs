using ENERGY_NOW_BE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class ClientSpecialization
    {
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Specialization Specialization { get; set; }
    }

}
