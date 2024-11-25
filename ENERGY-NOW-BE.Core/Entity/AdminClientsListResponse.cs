using ENERGY_NOW_BE.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class AdminClientsListResponse
    {
        public Guid ClientId { get; set; }
        public string UserId { get; set; }
        public string Cui { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsAuthorize { get; set; }
        public List<Specialization> Specialization{ get; set; }
    }
}
