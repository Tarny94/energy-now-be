using ENERGY_NOW_BE.Core.Entity;
using ENERGY_NOW_BE.Core.Entity.Auth;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ENERGY_NOW_BE.Application.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
             IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(RegisterModel userRegister)
        {
            ArgumentNullException.ThrowIfNull(userRegister);
            if (!IsValidEmail(userRegister.Email)) return IdentityResult.Failed(new IdentityError { Code = "InvalidEmail", Description = "The email format is invalid." });

            var user = CreateAnUser(userRegister);
            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
            }

            return result;
        }

        public async Task<LoginResponseModel> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                return null; // Or handle invalid password
            }

            var userRole = await _userManager.GetRolesAsync(user);

            // Generate token
            var token = await GenerateJwtToken(user);

            return new LoginResponseModel
            {
                Token = token,
                ExpiresIn = DateTime.UtcNow.AddHours(1),  // Example expiration time
                UserId = user.Id,
                UserRole = userRole,
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GeneratePasswordResetToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<string> GenerateEmailConfirmationToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> RefreshToken(string expiredToken)
        {
            var user = await _userManager.FindByIdAsync(GetUserIdFromToken(expiredToken));
            if (user == null || !await ValidateTokenAsync(expiredToken, user))
            {
                throw new Exception("Invalid token");
            }

            return await GenerateJwtToken(user);
        }

        private string GenerateNewToken(User user)
        {
            throw new NotImplementedException();
        }

        private async Task AssignRoleToUser(User user, bool isClient, bool isSuperClient)
        {
            try
            {
                if(isSuperClient) await _userManager.AddToRoleAsync(user, "ADMIN");
                else
                {
                    if (isClient) await _userManager.AddToRoleAsync(user, "CLIENT");
                    else await _userManager.AddToRoleAsync(user, "USER");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private User CreateAnUser(RegisterModel newUser)
        {
            return new User
            {
                UserName = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                IsAClient = newUser.IsAClient,
            };
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

        private string GetUserIdFromToken(string token)
        {
            // Parse the JWT token to extract the user ID
            throw new NotImplementedException(); // Implement token parsing logic
        }

        private Task<bool> ValidateTokenAsync(string token, User user)
        {
            // Implement token validation logic here
            throw new NotImplementedException();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

            // Add user roles to claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(1);  // Set expiration time

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

