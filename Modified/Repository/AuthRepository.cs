using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.IdentityModel.Tokens;
using Modified.Data;
using Modified.Interfaces;
using Modified.Models;
using System.Configuration;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Modified.Repository
{

    public class AuthRepository : IAuth
    {
        private readonly MyDB context;
        private readonly IConfiguration configuration;
        public AuthRepository(MyDB db, IConfiguration configuration)
        {
            context = db;
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = context.Users.FirstOrDefault(i => i.Email.ToLower().Equals(email.ToLower()));
            if ( user == null)
            {
                response.Succes = false;
                response.Message = "not found";
                return response;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Succes = false;
                response.Message = "wrong password";
                return response;
            }
            else
            {
                response.Data = CreateToken(user);

            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password, string email)
        {
            var response = new ServiceResponse<int>();

            if (await UserExists(user.Username))
            {
                response.Succes = false;
                response.Message = "user already exists";
                return response;
            }
            CreatePasswordHassh(password, out byte[] passHash, out byte[] saltHash);
            user.PasswordHash = passHash;
            user.PasswordSalt = saltHash;
            user.Email = email;
            context.Users.Add(user);
            await context.SaveChangesAsync();
            response.Data = user.Id;
            return response;

        }

        public async Task<bool> UserExists(string username)
        {
            if (context.Users.Count() == 0)
            {
                return false;
            }
            if ( context.Users.Any(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHassh(string password,out byte[] passHash,out byte[] passSalt) {
        using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        private bool VerifyPasswordHash(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return compute.SequenceEqual(passHash);
            }
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var appSettingsToken = configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            /*     var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.Name)
                 };
                 var appseting = configuration.GetSection("AppSettings:Token").Value;
                 if(appseting == null)
                 {
                     throw new Exception("token is null");
                 }
                 SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appseting));
                 SigningCredentials creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
                 var tokenD = new SecurityTokenDescriptor
                 {
                     Subject = new ClaimsIdentity(claims),
                     Expires = DateTime.Now.AddDays(1),
                     SigningCredentials = creds
                 };
                 JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                 SecurityToken tok = jwtSecurityTokenHandler.CreateToken(tokenD);

                 return jwtSecurityTokenHandler.WriteToken(tok);*/
        }
    }
}
