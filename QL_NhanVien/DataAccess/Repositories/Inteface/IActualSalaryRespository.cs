using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface IActualSalaryRespository
    {
        bool CreateActualSalry(ActualSalary actualSalary);
    }
}
