using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTP.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Transactions;
using System.Data;
using AdidataDbContext.Models.Mysql.PTPDev;
using NPOI.SS.Formula.Functions;
using NPOI.POIFS.Crypt.Dsig;
using Org.BouncyCastle.Asn1.Cms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PTP.Service;
using PTP.Dto.AuthDto;

namespace PTP.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly PTPDevContext context;
        private readonly IConfiguration configuration;

        public AuthenticationService(
            PTPDevContext context, 
            IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<ResponseObject> CreateUserAsync(CreateUserDto request)
        {
            try
            {
                var exisUser = await context.Users.Where(u => u.Email == request.Email || u.Name == request.Username).FirstOrDefaultAsync();
                if(exisUser != null)
                {
                    return new ResponseObject
                    {
                        Status = 400,
                        Message = "Nama atau email sudah terdaftar!",
                        Data = null,
                    };
                }

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new User
                {
                    Roleid = 2,
                    Name = request.Username,
                    Password = hashPassword,
                    Email = request.Email,
                    CreatedDate = DateTime.Now,
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                return new ResponseObject
                {
                    Status = 200,
                    Message = "Berhasil Membuat Account",
                    Data = Enumerable.Empty<object>(),
                };

            }
            catch(Exception ex)
            {
                return new ResponseObject
                {
                    Status = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }   
        }

        public async Task<ResponseObject> GetAllUsersAsync()
        {
            try
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

                return new ResponseObject
                {
                    Status = 200,
                    Message = "Success",
                    Data = getUser
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject
                {
                    Status = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject> LoginAsync(Login login, string ipAddress)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

                if (user == null)
                {
                    return new ResponseObject
                    {
                        Status = 400,
                        Message = "Email yang anda masukan salah!",
                        Data = null,
                    };
                }

                bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
                if (!isValid)
                {
                    return new ResponseObject
                    {
                        Status = 400,
                        Message = "Password yang anda masukan salah!",
                        Data = null,
                    };
                }

                var findToken = await context.UsersTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (findToken != null)
                {
                    //return new ResponseObject
                    //{
                    //    Status = 400,
                    //    Message = "User sedang digunakan di device lain",
                    //    Data = null,
                    //};
                    context.UsersTokens.Remove(findToken);

                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Name", user.Name),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: signin
                );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                var refreshToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
                var refreshTokenExpiry = DateTime.Now.AddMinutes(5);
              

                var userToken = new UsersToken
                {
                    UserId = user.Id,
                    Nama = user.Name,
                    Token = tokenValue,
                    RefreshToken = refreshToken,
                    RefreshExpiredTime = refreshTokenExpiry,
                    IpAddress = ipAddress,
                    Hostname = Dns.GetHostName(),
                    CreatedTime = DateTime.Now,
                    ExpiredTime = DateTime.Now.AddMinutes(2),
                };

                context.UsersTokens.Add(userToken);
                await context.SaveChangesAsync();

                return new ResponseObject
                {
                    Status = 200,
                    Message = "Berhasil Login",
                    Data = new
                    {
                        userToken.Token,
                        userToken.RefreshToken,
                    },
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject
                {
                    Status = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject> LogoutAsync(int Id)
        {
            try
            {
                var userToken = await context.UsersTokens.FirstOrDefaultAsync(x => x.UserId == Id);
                context.UsersTokens.Remove(userToken);
                await context.SaveChangesAsync();

                return new ResponseObject
                {
                    Status = 200,
                    Message = "Berhasil Logout",
                    Data = null
                };
            }
            catch(Exception ex)
            {
                return new ResponseObject
                {
                    Status = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }   
        
        }

        public async Task<ResponseObject> RefreshTokenAsync(RefreshTokenDto request)
        {
            try
            {
                var tokenEntry = await context.UsersTokens
                       .FirstOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);

                if (tokenEntry == null)
                {
                    return new ResponseObject
                    {
                        Status = 400,
                        Message = "Refresh token tidak valid!",
                        Data = null
                    };
                }

                if (tokenEntry.RefreshExpiredTime < DateTime.Now)
                {
                    return new ResponseObject
                    {
                        Status = 400,
                        Message = "Refresh token sudah expired, silahkan login ulang.",
                        Data = null
                    };
                }

                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == tokenEntry.UserId);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Name", user.Name),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var newAccessToken = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: signin
                );

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

                tokenEntry.Token = tokenValue;
                tokenEntry.ExpiredTime = DateTime.Now.AddMinutes(2);
                await context.SaveChangesAsync();

                return new ResponseObject
                {
                    Status = 200,
                    Message = "Success",
                    Data = new
                    {
                        Token = tokenValue,
                        RefreshToken = tokenEntry.RefreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject
                {
                    Status = 500,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }
    }

}
