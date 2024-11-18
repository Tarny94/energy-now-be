using Microsoft.AspNetCore.Identity;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientName { get; set; }
        public bool IsValidClient { get; set; }
        public int Cui { get; set; }
    }

}
