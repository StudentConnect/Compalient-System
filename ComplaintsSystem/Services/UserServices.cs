using ComplaintsSystem.Models;
using ComplaintsSystem.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsSystem.Services
{
    public class UserServices : IUserServices
    {           
        private readonly CMSContext _db;
        private readonly IConfiguration _configuration;

        public UserServices(CMSContext db, IConfiguration configuration)
        {           
            _db = db;
            _configuration = configuration;
        }

        public UserModel Authenticate(string email, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserEmail == email && x.Password == password && x.IsActive == true);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserEmail.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserModel usermdl = new UserModel() { Email = user.UserEmail, IsActive = (bool)user.IsActive, Token = tokenHandler.WriteToken(token) };


            return usermdl;
        }
    }

    
}
