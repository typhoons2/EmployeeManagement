using Microsoft.EntityFrameworkCore;
using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IEmailService emailService) { 
            _unitOfWork = unitOfWork; 
            _emailService = emailService;
        }


        public bool ConFirmEmail(int userId, string code)
        {
            var emailConfirmation = _unitOfWork.EmailConfirmationObj.GetEmailConfirmation(userId, code);
            if (emailConfirmation == null || emailConfirmation.ExpiryDate < DateTime.Now)
            {
                return false;
            }
            var user = _unitOfWork.UserObj.GetUserById(userId);
            if (user == null)
            {
                return false;
            }
            user.EmailConfirmed = true;
            _unitOfWork.EmailConfirmationObj.RemoveEmailConfirmation(emailConfirmation);
            return true;
        }

        public void CreateUser(User user)
        {
            _unitOfWork.UserObj.CreateUser(user);
            // Tạo mã xác nhận email
            var confirmationCode = Guid.NewGuid().ToString();
            var emailConfirmation = new EmailConfirmation
            {
                UserId = user.UserId,
                ConfirmationCode = confirmationCode,
                ExpiryDate = DateTime.Now.AddHours(24) // Ví dụ mã xác nhận hết hạn sau 24 giờ
            };
            _unitOfWork.EmailConfirmationObj.CreateEmailConfirmation(emailConfirmation);


            // Gửi email xác nhận
            var confirmationLink = $"https://localhost:7165/api/Auth/confirmemail?userId={user.UserId}&code={confirmationCode}";
            _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>");
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



        User IUserService.GetUserByGoogleIdAsync(string googleId)
        {
            return _unitOfWork.UserObj.GetUserByGoogleIdAsync(googleId);
        }
    }
}
