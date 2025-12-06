using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PTP.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PTP.AuthenticationRepository
{

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly PTPDevContext context;
        private readonly IConfiguration configuration;

        public AuthenticationRepository(PTPDevContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            responseObject = new ResponseObject();
            responseMessage = new ResponseMessage();
        }
        public ResponseObject responseObject { get; set; }
        public ResponseMessage responseMessage { get; set; }
        private void SetResponseObject(int status, string message, object data)
        {
            responseObject.Status = status;
            responseObject.Message = message;
            responseObject.Data = data;
        }
        public async Task<ResponseObject> GetAllUsersAsync()
        {
            var users = await context.Users.ToListAsync();
            var getUser = users.Select(x => new
            {
                x.Id,
                x.Name,
                x.Email,
                x.Password,
                x.Roleid,
            });
            SetResponseObject(200, "Success", getUser);
            return responseObject;
        }
        public async Task<ResponseObject> Login(Login login,String ipAddress)
        {
            var user = await context.Users.FirstOrDefaultAsync();

            if (user.Email == login.Email && user.Password == login.Password)
            {
                var findToken = await context.UsersTokens.FirstOrDefaultAsync(x => x.Nama == user.Name);
                var hostname = Dns.GetHostName();

                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Email", user.Email.ToString()),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signin
                    );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                var userToken = new UsersToken
                {
                    UserId = user.Id,
                    Nama = user.Name,
                    Token = tokenValue,
                    IpAddress = ipAddress,
                    Hostname = hostname,
                    CreatedTime = DateTime.Now,
                    ExpiredTime = DateTime.Now.AddHours(2),
                };

                if (findToken == null)
                {
                    context.UsersTokens.Add(userToken);
                    await context.SaveChangesAsync();
                }
                else
                {
                    SetResponseObject(401, "User sedang digunakan di device lain", null);
                    return responseObject;
                }

                SetResponseObject(200, "Berhasil Login", userToken);
                return responseObject;
            }
            else if (user.Email != login.Email)
            {
                SetResponseObject(401, "Email yang anda masukan salah!",null);
                return responseObject;
            }
            else
            {
                SetResponseObject(401, "Password yang anda masukan salah!",null);
                return responseObject;
            }
        }
        public async Task<ResponseObject> Logout(int Id)
        {
            var userToken = await context.UsersTokens.FirstOrDefaultAsync(x => x.UserId == Id);
            context.UsersTokens.Remove(userToken);
            await context.SaveChangesAsync();
            SetResponseObject(200, "Berhasil Logout", null);
            return responseObject;
        }

    }
}
