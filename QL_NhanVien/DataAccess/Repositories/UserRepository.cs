using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QLNhanVienContext _context;

        public UserRepository(QLNhanVienContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        private bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public User GetUser(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }
    }
}
