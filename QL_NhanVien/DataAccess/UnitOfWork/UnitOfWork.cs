using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserObj { get;private set; }

        public IActualSalaryRespository ActualSalaryObj { get; private set; }

        public ISubmissionRepository SubmissionObj { get; private set; }

        public IAttachedFileRepository AttachedFileObj { get; private set; }

        public IEmailConfirmationRepository EmailConfirmationObj { get; private set; }

        public IClaimRespository ClaimObj { get; private set; }

        public QLNhanVienContext _db;
        public UnitOfWork( QLNhanVienContext db)
        { 
            _db = db;
            UserObj = new UserRepository(_db);
            ActualSalaryObj = new ActualSalaryRespository(_db);
            SubmissionObj = new SubmissionRepository(_db);
            AttachedFileObj = new AttachedFileRepository(_db);
            EmailConfirmationObj = new EmailConfirmationRespository( _db);
            ClaimObj = new ClaimRespository( _db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
