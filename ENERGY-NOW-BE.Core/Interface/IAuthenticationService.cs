using ENERGY_NOW_BE.Core.Entity.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Interface
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(RegisterModel userRegister);
        Task<LoginResponseModel> Login(string email, string password);
        Task Logout();
        Task<string> GeneratePasswordResetToken(string email);
        Task<IdentityResult> ResetPassword(string email, string token, string newPassword);
        Task<string> GenerateEmailConfirmationToken(string email);
        Task<IdentityResult> ConfirmEmail(string email, string token);
        Task<string> RefreshToken(string token);
    }
}
