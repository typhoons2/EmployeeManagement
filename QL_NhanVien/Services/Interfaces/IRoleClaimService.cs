namespace QL_NhanVien.Services.Interfaces
{
    public interface IRoleClaimService
    {
        bool AddClaimToRole(int roleId, int claimId);
    }
}
