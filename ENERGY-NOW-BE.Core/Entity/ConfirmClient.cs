using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class ConfirmClient
    {
        public string ClientId { get; set; }

        public string UserId { get; set; }

        public bool IsAuthorizated { get; set; }
   
        public bool IsClientConfirmed { get; set; }

    }
}
