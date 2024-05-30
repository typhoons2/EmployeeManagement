using Microsoft.IdentityModel.Tokens;
using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace QL_NhanVien.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(IConfiguration configuration, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _httpContextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("UserId",user.UserId.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                RefToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpierTime= DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };
            
            return refreshToken;
        }

        

        public string GetUserName()
        {
            var result = string.Empty;
            if(_httpContextAccessor.HttpContext != null)
            {
                var tempo = _httpContextAccessor.HttpContext.User;
                result = tempo.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
  

        public void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpierTime
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken",newRefreshToken.RefToken,cookieOptions);
            user.RefreshToken = newRefreshToken.RefToken;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.ExpierTime;
            _unitOfWork.UserObj.UpdateUser(user);
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
