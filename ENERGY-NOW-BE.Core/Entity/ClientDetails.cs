using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class ClientDetails
    {
        public int Id { get; set; }
        public string ElectricianType { get; set; }
        public string PowerAuthorization {  get; set; }
        public string Description { get; set; }
        public bool isAuthorizated { get; set; }
        public bool isConfigured { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
