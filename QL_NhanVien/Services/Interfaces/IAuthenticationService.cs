using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Services.Interfaces
{
    public interface IAuthenticationService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password,  byte[] passwordHash, byte[] passwordSalt);

        string CreateToken(User user);

        RefreshToken GenerateRefreshToken();
        void SetRefreshToken(User user, RefreshToken newRefreshToken);
        string GetUserName();


    }
}
