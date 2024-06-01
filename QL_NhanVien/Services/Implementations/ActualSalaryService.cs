using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Services.Implementations
{
    public class ActualSalaryService : IActualSalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActualSalaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateActualSalary(ActualSalary actualSalary)
        {
            return _unitOfWork.ActualSalaryObj.CreateActualSalry(actualSalary);
            
        }

        public ActualSalary GetActualSalaryByUserId(int userId)
        {
            return _unitOfWork.ActualSalaryObj.GetActualSalaryByUserId(userId);
        }
    }
}
