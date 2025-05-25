using ENERGY_NOW_BE.Core.Entity;

namespace ENERGY_NOW_BE.Application.Auth
{
    public class UserServiceBase
    {

        public async string ClientConfiguration(ClientConfiguration clientConfiguration)
        {
            var user = await _userManager.FindByIdAsync(clientConfiguration.UserID);

            if (user == null) return "User not exist!!!";

            return " ";
        }
    }
}