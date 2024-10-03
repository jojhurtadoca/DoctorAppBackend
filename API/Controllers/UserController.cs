using Data;
using Data.Interfaces;
using Data.Interfaces.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private ApiResponse _response;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<AppUser> userManager, ITokenService tokenService, RoleManager<AppRole> roleManager, IUnitOfWork work)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _response = new ApiResponse();
            _unitOfWork = work;
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => new
            {
                RoleName = r.Name,
            }).ToList();
            _response.Result = roles;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO dto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
            if (user == null)
            {
                return Unauthorized("Username not found");
            }
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result)
            {
                return BadRequest("Invalid credentials");
            }
            return Ok(new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.MakeToken(user)
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(CreateUserDTO user)
        {
            if (await UserExists(user.Username))
            {
                return BadRequest("User is already registered");
            }

            //using var hmac = new HMACSHA512();
            var newUser = new AppUser
            {
                UserName = user.Username.ToLower(),
                Email = user.Email,
                Lastname = user.Lastname,
                Name = user.Name
            };

            var result = await _userManager.CreateAsync(newUser, user.Password );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, user.Role);

            if (!roleResult.Succeeded)
            {
                return BadRequest("Error adding role");
            }

            var userDTO = new UserDTO
            {
                UserName = newUser.UserName,
                Token = await _tokenService.MakeToken(newUser)
            };
            return Ok(userDTO);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetUsersWithRoles();
            var userDtoList = users.Select(s => new UserListDto
            {
                Username = s.UserName,
                Name = s.Name,
                Email = s.Email,
                Lastname = s.Lastname,
                Role = string.Join(',', _userManager.GetRolesAsync(s).Result.ToArray()),
            });
            /*var users = await _userManager.Users.Select(s => new UserListDto
            {
                Username = s.UserName,
                Name = s.Name,
                Email = s.Email,
                Lastname = s.Lastname,
                Role = string.Join(',', _userManager.GetRolesAsync(s).Result.ToArray()),
            }).ToListAsync();*/
            _response.Result = userDtoList;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

       /* [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }*/
    }
}
