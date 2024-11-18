using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class Client : IdentityUser
    {
        public bool ClientName { get; set; }

        public bool IsValidClient { get; set; }

        public int Cui { get; set; }
    }
}
