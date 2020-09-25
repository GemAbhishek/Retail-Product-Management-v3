using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using Authorization.Model;
using Authorization.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Login controller to generate token

    public class LoginController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpPost]
        public string Login(User user)
        {
            User u = new UserRepository().GetUser(user.Username);
            if (u == null)
                return "error";
            bool credentials = u.Password.Equals(user.Password);
            if (!credentials) return "error";
            return GenerateToken(user.Username);
        }

        public string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenInfo:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: Configuration["TokenInfo:Issuer"],
            audience: Configuration["TokenInfo:Audience"],
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
