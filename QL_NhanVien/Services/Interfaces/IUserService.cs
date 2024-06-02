using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Services.Interfaces
{
    public interface IUserService
    {

        ICollection<User> GetUsers();

        User GetUserByUserName(string userName);
        void CreateUser(User user);
        bool UpdateUser(User user);
        User GetUserById(int id);
        User GetUserByGoogleIdAsync(string googleId);

        bool ConFirmEmail(int userId, string code);
    }
}
