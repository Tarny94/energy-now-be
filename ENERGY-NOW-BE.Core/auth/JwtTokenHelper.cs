using ENERGY_NOW_BE.Core.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ENERGY_NOW_BE.Core.auth
{
    public class JwtTokenHelper
    {
        //private readonly string _secretKey = "MySecretKeyMySecretKeyMySecretKeyMySecretKeyMySecretKey";

        //private readonly string _secretKey2;
        //public JwtTokenHelper()
        //{
        //}

        //public JwtTokenHelper(string secretKey)
        //{
        //    _secretKey2 = secretKey;
        //}

        //public string GenerateToken(User user, List<string> roles)
        //{
        //    var claims = new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };

        //    // Add roles as claims
        //    //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: "https://localhost:7045/api",
        //        audience: "https://localhost:7045/api",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }

}
