using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly QLNhanVienContext _context;
        public SubmissionRepository(QLNhanVienContext context)
        {
            _context = context;
        }
        public bool CreateSubmission(Submission submission)
        {
            _context.Submissions.Add(submission);
            return Save();
            
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
