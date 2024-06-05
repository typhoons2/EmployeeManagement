using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.DataAccess.Repositories.Inteface;

namespace QL_NhanVien.DataAccess.Repositories
{
    public class ClaimRespository : IClaimRespository
    {
        private readonly QLNhanVienContext _context;
        public ClaimRespository(QLNhanVienContext context)
        {
            _context = context;
        }

        public bool CreateClaim(Claim claim)
        {
            _context.Claims.Add(claim);
            return Save();
        }

        public ICollection<Claim> GetAllClaim()
        {
            return _context.Claims.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
