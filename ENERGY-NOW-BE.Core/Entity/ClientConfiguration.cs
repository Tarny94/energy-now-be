using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class ClientConfiguration
    {
        public string FirmName { get; set; }
        public string Phone { get; set; }
        public string ElectricalType { get; set; }
        public int PowerAuthorize { get; set; }
        public string Description { get; set; }
        public int TicketsDone { get; set; }
        public bool Authorize { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public bool isConfigured { get; set; }
        public string UserID { get; set; }
        public int Id { get; set; }
    }
}
