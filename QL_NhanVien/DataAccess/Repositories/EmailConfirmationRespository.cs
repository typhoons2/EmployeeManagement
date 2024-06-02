using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.Repositories
{
    public class EmailConfirmationRespository : IEmailConfirmationRepository
    {
        private readonly QLNhanVienContext _context;
        public EmailConfirmationRespository(QLNhanVienContext context)
        {
            _context = context;
        }

        public bool CreateEmailConfirmation(EmailConfirmation emailConfirmation)
        {
            _context.EmailConfirmations.Add(emailConfirmation);
            return Save();
        }

        public EmailConfirmation GetEmailConfirmation(int userId, string code)
        {
            return _context.EmailConfirmations.SingleOrDefault(ec => ec.UserId == userId && ec.ConfirmationCode == code);
        }

        public bool RemoveEmailConfirmation(EmailConfirmation emailConfirmation)
        {
            _context.EmailConfirmations.Remove(emailConfirmation); return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
