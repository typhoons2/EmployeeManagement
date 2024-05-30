using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.Services.Interfaces;

namespace QL_NhanVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        Services.Interfaces.IAuthenticationService _authenticationService;
        IUserService _userService;
        ISubmissionService _submissionService;
        public SubmissionController(Services.Interfaces.IAuthenticationService authenticationService, IUserService userService, ISubmissionService submissionService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _submissionService = submissionService;
        }
        //[HttpPost("post-submission"),Authorize]
        //public ActionResult PostSubmission([FromForm] SubmissionDTO dto, IFormFile fileData)
        //{
        //    var userName = _authenticationService.GetUserName();
        //    var User = _userService.GetUserByUserName(userName);
        //    _submissionService.CreateSubmission()

        //}
    }
}
