using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.DataAccess.Entities;
using QL_NhanVien.Services.Interfaces;
using System.Security.Claims;

namespace QL_NhanVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet("all")]
        [Authorize(Roles = "2")]
        public IActionResult GetAllUser() {
            var users = _userService.GetUsers();
            var userDtos = _mapper.Map<IEnumerable<User>,IEnumerable<UserResponseDTO>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}"), Authorize]

        public IActionResult GetUser(int id)
        {
            var currentUserId = int.Parse(User.FindFirstValue("UserId"));

            if(currentUserId != id)
            {
                return Unauthorized("Acess denied: you can only view your own information!");
            }
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            var userDto = _mapper.Map<UserResponseDTO>(user);
            return Ok(userDto);
        }



        [HttpPost("{id}"), Authorize(Roles ="2")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateRequestDTO request)
        {
            var existingUser = _userService.GetUserById(id);
            if(existingUser == null)
            {
                return BadRequest("User not found");
            }
            _mapper.Map(request, existingUser);
            _userService.UpdateUser(existingUser);
            return Ok("ok");


        }

        
    }
}
