using Microsoft.AspNetCore.Identity;

namespace ENERGY_NOW_BE.Core.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAClient { get; set; }

        public ICollection<Client> Client { get; set; }

    }
}
