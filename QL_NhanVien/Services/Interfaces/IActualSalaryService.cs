using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Services.Interfaces
{
    public interface IActualSalaryService
    {
        bool CreateActualSalary(ActualSalary actualSalary);

        ActualSalary GetActualSalaryByUserId(int userId);
    }
}
