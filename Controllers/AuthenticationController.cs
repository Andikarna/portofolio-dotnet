
using AdidataDbContext.Models.Mysql.PTPDev;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities.Net;
using PTP.Components;
using PTP.Dto.AuthDto;
using PTP.Helper;
using PTP.Service;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace PTP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly AuthenticationService authServices;
        private readonly PTPDevContext context;
        private readonly ResponseStatusHeader header;
        

        public AuthenticationController(
            AuthenticationService authServices,
            PTPDevContext context,
            ResponseStatusHeader header,
            IConfiguration configuration
            )
        {
            this.context = context;
            this.authServices = authServices;
            this.header = header;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser([FromQuery] CreateUserDto request)
        {

            var result = await authServices.CreateUserAsync(request);
            return header.BuildResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await authServices.LoginAsync(login, ipAddress);
            return header.BuildResponse(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RefreshToken([FromQuery] RefreshTokenDto request)
        {
            var key = configuration["Jwt:Key"];
            var validation = TokenService.ValidateToken(User, Request, key);
            var existToken = await context.UsersTokens.FirstOrDefaultAsync(x => x.UserId == validation.UserId);
            if (existToken == null)
            {
                return header.BuildResponse(new
                {
                    Status = 401,
                    Message = "User not login"
                });
            }

            var result = await authServices.RefreshTokenAsync(request);
            return header.BuildResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Logout([FromQuery] int id)
        {
            var result = await authServices.LogoutAsync(id);
            return header.BuildResponse(result);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var key = configuration["Jwt:Key"];
            var validation = TokenService.ValidateToken(User, Request, key);
            var existToken = await context.UsersTokens.FirstOrDefaultAsync(x => x.UserId == validation.UserId);
            if (existToken == null)
            {
                return header.BuildResponse(new
                {
                    Status = 401,
                    Message = "User not login"
                });
            }

            var result = await authServices.GetAllUsersAsync();
            return header.BuildResponse(result);
        }


       
    }
}


