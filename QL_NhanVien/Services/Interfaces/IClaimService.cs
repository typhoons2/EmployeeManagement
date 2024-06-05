using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Services.Interfaces
{
    public interface IClaimService
    {
        ICollection<Claim> GetAllClaim();
        bool CreateClaim(Claim claim);
    }
}
