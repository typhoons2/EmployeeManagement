using Microsoft.EntityFrameworkCore;
using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Helper.Middleware
{
    public class UserClaimService
    {
        private readonly QLNhanVienContext _context;
        public UserClaimService(QLNhanVienContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetUserClaimsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.Claims)
                        .ThenInclude(rc => rc.ClaimName).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return new List<string>();
            }
            return user.Role.Claims.Select(rc => rc.ClaimName).ToList();
        }
    }
}
