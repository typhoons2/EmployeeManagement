using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.Helper.Attributes;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;
        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost("new-claim")]
        [Authorize]
        public IActionResult CreateNewClaim([FromBody] ClaimDTO claimDTO)
        {
            var newClaim = new Claim
            {
                ClaimName = claimDTO.ClaimName
            };
            _claimService.CreateClaim(newClaim);
            return Ok(newClaim);

        }

    }
}
