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

namespace PTP.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository authRepository;

        public AuthenticationService(IAuthenticationRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<ResponseObject> GetAllUsersAsync()
        {
            var user = await authRepository.GetAllUsersAsync();
            return user;
        }

        public async Task<ResponseObject> Login(Login login ,String ipAddress)
        {
            var result = await authRepository.Login(login, ipAddress);
            return result;
        }

        public async Task<ResponseObject> Logout(int Id)
        {
            var result = await authRepository.Logout(Id);
            return result;
        }

    }



}
