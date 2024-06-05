using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface IClaimRespository
    {
        ICollection<Claim> GetAllClaim();

        bool CreateClaim(Claim claim);
        bool Save();
    }
}
