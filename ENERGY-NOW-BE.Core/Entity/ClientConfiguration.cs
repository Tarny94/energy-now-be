using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class ClientConfiguration
    {
        public string Avatar { get; set; }
        public string Cui { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string Details { get; set; }
        public bool IsAuthorizated { get; set; }
        public bool IsConfirmed { get; set; }

        [Key]
        public string UserId { get; set; }
    }
}
