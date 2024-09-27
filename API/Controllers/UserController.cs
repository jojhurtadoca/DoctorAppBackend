using Data;
using Data.Interfaces;
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

        public UserController(UserManager<AppUser> userManager, ITokenService tokenService, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _response = new ApiResponse();
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

       /* [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }*/
    }
}
