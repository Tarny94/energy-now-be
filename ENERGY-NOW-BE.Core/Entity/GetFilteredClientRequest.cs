using ENERGY_NOW_BE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class GetFilteredClientRequest
    {
        public string County { get; set; }
        public string City { get; set; }
        public List<ClientSpecialization> Specializations { get; set; }
    }
}
