using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Identity;


namespace ENERGY_NOW_BE.Application.Auth
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Example method that reuses the existing Identity functionality
        public async Task<IdentityResult> RegisterUser(UserRegister userRegister)
        {
            if (userRegister == null)
            {
                throw new ArgumentNullException(nameof(userRegister));
            }

            if (!IsValidEmail(userRegister.Email))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidEmail",
                    Description = "The email format is invalid."
                });
            }

            var user = userRegister.IsClient? CreateAClient(userRegister) : CreateAnUser(userRegister);

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await AssignRoleToUser(user, userRegister.IsClient);
            }

            return result;
        }

        private async Task AssignRoleToUser(User user, bool isClient)
        {
            try
            {
                if (isClient)
                {
                    await _userManager.AddToRoleAsync(user, "CLIENT");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "USER");
                }
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }

        }

        private User CreateAnUser(UserRegister NewUser)
        {
            try
            {
                var client = new User { UserName = NewUser.Email, ClientName = NewUser.ClientName, FirstName = NewUser.FirstName, LastName = NewUser.LastName, Email = NewUser.Email, PhoneNumber = NewUser.PhoneNumber, IsValidClient = NewUser.IsValidClient, Cui = NewUser.Cui };
                return client;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private User CreateAClient(UserRegister NewUser)
        {
            try
            {
                var user = new User { UserName = NewUser.Email, ClientName = NewUser.ClientName, FirstName = NewUser.FirstName, LastName = NewUser.LastName, Email = NewUser.Email, PhoneNumber = NewUser.PhoneNumber };
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
