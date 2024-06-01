using AutoMapper;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace QL_NhanVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Services.Interfaces.IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IActualSalaryService _actualSalaryService;
        public AuthController(IMapper mapper, IUserService userService, Services.Interfaces.IAuthenticationService authenticationService, IActualSalaryService actualSalaryService)
        {
            _mapper = mapper;
            _userService = userService;
            _authenticationService = authenticationService;
            _actualSalaryService = actualSalaryService;
        }

        [HttpPost("Register")]

        public async Task<ActionResult> Register(UserRegisterRequestDTO request)
        {
            var existingUser = _userService.GetUserByUserName(request.UserName);
            if (existingUser != null) {
                return BadRequest("Username is already exist");
            }
            _authenticationService.CreatePasswordHash(request.Password,out byte[] passwordHash,out byte[] passwordSalt);
            var newUser = _mapper.Map<UserRegisterRequestDTO,User>(request);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.DaysOff = 0;

            _userService.CreateUser(newUser);

            var acrualSalary = new ActualSalary
            {
                DaysOff = 0,
                ContractSalary = newUser.ContractSalary,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                UserId = newUser.UserId
            };

            _actualSalaryService.CreateActualSalary(acrualSalary);
            var userResponse = _mapper.Map<User, UserRegisterRequestDTO>(newUser);

            return(Ok(userResponse));
            
        }
        [HttpPost("Login")]

        public async Task<ActionResult> Login(UserLoginRequest login)
        {   
            var user = _userService.GetUserByUserName(login.UserName);
            if (user == null)
            {
                return BadRequest("user not found");

            }
            if (!_authenticationService.VerifyPasswordHash(login.Password,user.PasswordHash,user.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }
            string token = _authenticationService.CreateToken(user);
            var refreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(user, refreshToken);

            return (Ok(token));
        }

        [HttpPost("refresh-token"), Authorize]
        public async Task<ActionResult> RefreshToken()
        {
            var userName = _authenticationService.GetUserName();
            var user = _userService.GetUserByUserName(userName);
            var refreshToken = Request.Cookies["refreshToken"];
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return BadRequest("Invalid refreshToken");
            }
            else if (user.TokenExpires < DateTime.Now){
                return BadRequest("token expires");
            }
            string token = _authenticationService.CreateToken(user);
            var newRefreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(user, newRefreshToken);
            return Ok(token);
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var googleId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _userService.GetUserByGoogleIdAsync(googleId);
            if (user == null)
            {
                user = new User
                {
                    DaysOff = 0,
                    GoogleId = googleId,
                    Email = email,
                    Name = email,
                    EmailConfirmed= true,
                    ContractSalary = 0,
                    RoleId = 1
                };
                _userService.CreateUser(user);

                var acrualSalary = new ActualSalary
                {
                    DaysOff = 0,
                    SalaryAfterDeductions = user.ContractSalary,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    UserId = user.UserId
                };

                _actualSalaryService.CreateActualSalary(acrualSalary);
            }

            string token = _authenticationService.CreateTokenMail(user);
            var refreshToken = _authenticationService.GenerateRefreshToken();
            _authenticationService.SetRefreshToken(user, refreshToken);

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken.RefToken
            });
        }

    }
}
