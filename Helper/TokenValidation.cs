using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace PTP.Helper
{
    public static class TokenService
    {
        public static ValidationToken ValidateToken(ClaimsPrincipal user, HttpRequest request, string secretKey)
        {
            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("Token is missing or invalid.");

            

            var userIdClaim = user.FindFirst("UserId")?.Value;
            var nameClaim = user.FindFirst("Name")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("UserId claim is missing in token.");

            if (string.IsNullOrEmpty(nameClaim))
                throw new UnauthorizedAccessException("Name claim is missing in token.");

            var token = GetBearerToken(request);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                if (validatedToken.ValidTo < DateTime.UtcNow)
                    throw new SecurityTokenExpiredException("Token has expired.");

            }
            catch (SecurityTokenExpiredException)
            {
                throw new UnauthorizedAccessException("Token has expired.");
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("Token is invalid.");
            }


            return new ValidationToken
            {
                UserId = int.Parse(userIdClaim),
                UserName = nameClaim,
                Token = token
            };
        }

        public static string GetBearerToken(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Authorization header is missing or invalid.");

            return authHeader.Replace("Bearer ", "").Trim();
        }

        public class ValidationToken  
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Token { get; set; }
        } 
    }
}
