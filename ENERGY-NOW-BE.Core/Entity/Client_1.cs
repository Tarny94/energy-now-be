using ENERGY_NOW_BE.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Cui { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Details { get; set; }
        public bool IsAuthorizated { get; set; }
        public bool IsConfirmed { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public ICollection<ClientSpecialization> ClientSpecializations { get; set; } = new List<ClientSpecialization>();


    }
}
