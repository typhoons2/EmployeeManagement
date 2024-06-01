using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.Repositories
{
    public class ActualSalaryRespository : IActualSalaryRespository

    {
        private readonly QLNhanVienContext _context;

        public ActualSalaryRespository(QLNhanVienContext context)
        {
            _context = context;
        }
        public bool CreateActualSalry(ActualSalary actualSalary)
        {
            _context.ActualSalaries.Add(actualSalary);
            return Save();
        }

        public ActualSalary GetActualSalaryByUserId(int userId)
        {
            return _context.ActualSalaries.FirstOrDefault(u => u.UserId == userId);
        }

        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
