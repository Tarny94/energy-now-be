using ENERGY_NOW_BE.Application.Auth;
using ENERGY_NOW_BE.Core.Entity.Auth;
using ENERGY_NOW_BE.Core.Interface;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ENERGY_NOW_BE.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authenticationService.RegisterUser(model);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authenticationService.Login(model.Email, model.Password);

            if (result == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.Strict, // Adjust based on your needs
                Expires = DateTime.UtcNow.AddHours(1) // Set expiration as needed
            };

            Response.Cookies.Append("token", result.Token, cookieOptions);

            return Ok(new
            {
                Token = result.Token,
                ExpiresIn = result.ExpiresIn,
                Email = result.Email,

            });
        }

        // Logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            Response.Cookies.Delete("token");
            return Ok("Logged out successfully.");
        }

        // Generate Password Reset Token
        [HttpPost("generate-password-reset-token")]
        public async Task<IActionResult> GeneratePasswordResetToken([FromBody] EmailModel model)
        {
            var token = await _authenticationService.GeneratePasswordResetToken(model.Email);
            return Ok(new { Token = token });
        }

        // Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var result = await _authenticationService.ResetPassword(model.Email, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password reset successful.");
            }
            return BadRequest(result.Errors);
        }

        // Generate Email Confirmation Token
        [HttpPost("generate-email-confirmation-token")]
        public async Task<IActionResult> GenerateEmailConfirmationToken([FromBody] EmailModel model)
        {
            var token = await _authenticationService.GenerateEmailConfirmationToken(model.Email);
            return Ok(new { Token = token });
        }

        // Confirm Email
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
        {
            var result = await _authenticationService.ConfirmEmail(model.Email, model.Token);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }
            return BadRequest(result.Errors);
        }

        // Refresh Token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel model)
        {
            var newToken = await _authenticationService.RefreshToken(model.Token);
            return Ok(new { Token = newToken });
        }

    }
}
