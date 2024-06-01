using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserObj { get; }

        IActualSalaryRespository ActualSalaryObj { get; }

        ISubmissionRepository SubmissionObj { get; }

        IAttachedFileRepository AttachedFileObj { get; }
        void Save();

    }
}
