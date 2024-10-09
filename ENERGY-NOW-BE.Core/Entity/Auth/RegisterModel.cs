using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity.Auth
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsClient { get; set; }

        //public string FirmAddress { get; set; }
        public bool IsValidClient { get; set; }
        public string ClientName { get; set; }
        public int Cui { get; set; }
        public bool IsSuperClient { get; set; }
    }
}
