namespace QL_NhanVien.DataAccess.Repositories.Inteface
{
    public interface IRoleClaimRespository
    {
        bool AddClaimToRole(int roleId, int claimId);
        bool Save();
    }
}
