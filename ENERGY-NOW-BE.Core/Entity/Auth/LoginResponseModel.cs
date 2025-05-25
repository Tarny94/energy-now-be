using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity.Auth
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string UserId { get; set; }
        public IList<string> UserRole { get; set; }
    }

}
