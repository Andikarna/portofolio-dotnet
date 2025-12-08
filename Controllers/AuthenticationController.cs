
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
using System.Threading.Tasks;


namespace BasicProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authServices;
        private readonly PTPDevContext context;

        public AuthenticationController(
            AuthenticationService authServices,
            PTPDevContext context
            )
        {
            this.context = context;
            this.authServices = authServices;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var response = await authServices.Login(login, ipAddress);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error " + ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Logout([FromQuery] int id)
        {
            try
            {
                var response = await authServices.Logout(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error " + ex.Message);
            }
        }

        //var users = User.FindFirst("UserId")?.Value;
        //int userId = int.Parse(users);
        //var user = await ptpDevContext.Users.FindAsync(userId);

        /*[Authorize]
        [DisableCors]*/
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await authServices.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpGet]
        public async Task<IActionResult> CreateUser([FromQuery] CreateUserDto request)
        {
            var user = new User
            {
                Name = request.Username,
                Password = request.Password,
                Email = request.Email,
                CreatedDate = DateTime.Now,
            };

            context.Users.Add(user);    
            await context.SaveChangesAsync();

            var result = new
            {
                Status = 200,
                Message = "Berhasil Membuat Account",
                Data = Enumerable.Empty<object>(),
            };

            return Ok(result);

        }
    }
}


