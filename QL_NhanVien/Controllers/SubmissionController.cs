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
        private readonly IActualSalaryService _actualSalaryService;
        private readonly IAttachedFileService _attachedFileService;
        public SubmissionController(Services.Interfaces.IAuthenticationService authenticationService, IUserService userService, ISubmissionService submissionService, IAttachedFileService attachedFileService,IActualSalaryService actualSalaryService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _submissionService = submissionService;
            _attachedFileService = attachedFileService;
            _actualSalaryService= actualSalaryService;

        }
        [HttpPost("post-submission")]
        public ActionResult PostSubmission([FromForm] SubmissionDTO dto, IFormFile fileData)
        {
            var userName = _authenticationService.GetUserName();
            var user = _userService.GetUserByUserName(userName);
            _submissionService.CreateSubmission(dto, fileData, user.UserId);
            return Ok();
        }

        [HttpGet("download-file")]
        public IActionResult DownloadFile(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid file ID");
            }

            try
            {
                _attachedFileService.DownloadFileById(id);
                return Ok("File download successful!");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
                return StatusCode(500, "An error occurred while downloading the file.");
            }
        }
        [HttpGet("calculate-salary{id}")]

        public IActionResult CalculateSalaryByUserId(int id)
        {
            var salaryUser = _actualSalaryService.GetActualSalaryByUserId(id);
            var user = _userService.GetUserById(id);
            decimal deductionAmount = (decimal)((decimal)salaryUser.ContractSalary  * (decimal)salaryUser.DaysOff * 0.05m);


            return Ok(deductionAmount);
        }

    }

}
