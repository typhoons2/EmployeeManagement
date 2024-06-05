using QL_NhanVien.DataAccess.Entities;
using System.Security.Claims;

namespace QL_NhanVien.Helper.Middleware
{
    public class ClaimCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, QLNhanVienContext dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userName = context.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                var userId = int.Parse(context.User.Claims.First(c => c.Type == "UserId").Value);
                var userSevice = new UserClaimService(dbContext);
                var userClaims = await userSevice.GetUserClaimsAsync(userId);


                var claimsIdentity = context.User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    foreach (var claim in userClaims)
                    {
                        claimsIdentity.AddClaim(new System.Security.Claims.Claim(claim, "true"));
                    }
                }
            }

            await _next(context);

        }
    }
}

