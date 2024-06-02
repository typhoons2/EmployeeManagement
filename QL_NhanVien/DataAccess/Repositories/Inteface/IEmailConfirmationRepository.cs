using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface IEmailConfirmationRepository
    {
        EmailConfirmation GetEmailConfirmation(int userId, string code);

        bool CreateEmailConfirmation(EmailConfirmation emailConfirmation);
        bool RemoveEmailConfirmation(EmailConfirmation emailConfirmation);
        bool Save();
    }
}
