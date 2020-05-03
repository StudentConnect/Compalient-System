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

        //Constuctor to Initialize objects
        public UserServices(CMSContext db, IConfiguration configuration)
        {           
            _db = db;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Authenticate user model if user exist in system then generate jwt token and return usermodel otherwise return null
        /// </summary>
        /// <param name="email">String value</param>
        /// <param name="password">String Value</param>
        /// <returns>UserModel from viewModel</returns>
        public UserModel Authenticate(string email, string password)
        {
            //check user is exist of not
            var user = _db.Users.SingleOrDefault(x => x.UserEmail == email && x.Password == password && x.IsActive == true);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            //get secret key from Appsetting.json
            var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);
            //Intilalize the description of the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //in subject we are send user Email we can also send different information
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.UserEmail.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),  //Set the token expiry periods
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //with the given secret key to geneate HmaSha256 Hash 
            };
            //Create jwt token 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //set values from user db and pass token
            UserModel usermdl = new UserModel() { Email = user.UserEmail, IsActive = (bool)user.IsActive, Token = tokenHandler.WriteToken(token) };


            return usermdl;
        }
    }

    
}
