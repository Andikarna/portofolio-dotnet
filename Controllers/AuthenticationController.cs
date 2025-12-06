
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


namespace BasicProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authServices;

        public AuthenticationController(AuthenticationService authServices)
        {
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
            catch (Exception ex) {
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

    }
}


