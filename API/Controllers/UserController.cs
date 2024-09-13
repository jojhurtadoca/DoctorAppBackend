using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public UserController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == dto.Username);
            if (user == null)
            {
                return Unauthorized("Bad credentials");
            }
            using var hmac = new HMACSHA512();
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Bad credentials");
                }
            }
            return Ok(new UserDTO
            {
                UserName = user.Username,
                Token = _tokenService.MakeToken(user)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(CreateUserDTO user)
        {
            if (await UserExists(user.Username))
            {
                return BadRequest("User is already registered");
            }

            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                Username = user.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                PasswordSalt = hmac.Key,
            };
            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        [Authorize]
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
        }
    }
}
