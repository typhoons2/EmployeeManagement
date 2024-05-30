using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface ISubmissionRepository
    {
        bool CreateSubmission(Submission submission);

        bool Save();
    }
}
