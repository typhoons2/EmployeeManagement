using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public bool CreateUser(User user)
        {
            return _unitOfWork.UserObj.CreateUser(user);
        }

        public User GetUserById(int id)
        {
            return _unitOfWork.UserObj.GetUserById(id);
        }

        public User GetUserByUserName(string userName)
        {
            return _unitOfWork.UserObj.GetUser(userName);
        }


        public ICollection<User> GetUsers()
        {
            return _unitOfWork.UserObj.GetUsers();
        }

        public bool UpdateUser(User user)
        {
            return _unitOfWork.UserObj.UpdateUser(user);
        }
    }
}
