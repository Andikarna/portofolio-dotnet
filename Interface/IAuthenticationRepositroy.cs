using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.AspNetCore.Mvc;
using PTP.Dto.AuthDto;

namespace PTP.Interface
{
    public interface IAuthenticationService
    {
        Task<ResponseObject> LoginAsync(Login login, String ipAddress);
        Task<ResponseObject> RefreshTokenAsync(RefreshTokenDto request);
        Task<ResponseObject> LogoutAsync(int Id);
        Task<ResponseObject> GetAllUsersAsync();
        Task<ResponseObject> CreateUserAsync(CreateUserDto request);
    }


}