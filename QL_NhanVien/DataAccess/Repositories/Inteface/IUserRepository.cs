using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(string userName);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        User GetUserById(int id);

    }
}
