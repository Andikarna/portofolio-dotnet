using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.AspNetCore.Mvc;

namespace PTP.Interface
{
    public interface IAuthenticationRepository
    {
        Task<ResponseObject> Login(Login login, String ipAddress);
        Task<ResponseObject> Logout(int Id);
        Task<ResponseObject> GetAllUsersAsync();
    }

    public interface IAuthenticationService
    {
        Task<ResponseObject> Login(Login login, String ipAddress);
        Task<ResponseObject> Logout(int Id);
        Task<ResponseObject> GetAllUsersAsync();

    }


}