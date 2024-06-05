using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.UnitOfWork;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Services.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClaimService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateClaim(Claim claim)
        {
            return _unitOfWork.ClaimObj.CreateClaim(claim);
        }

        public ICollection<Claim> GetAllClaim()
        {
            throw new NotImplementedException();
        }
    }
}
