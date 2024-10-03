using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Interfaces.Repository
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> GetUsersWithRoles()
        {
            return _context.AppUsers.Include(s => s.UserRoles).ToList();
        }
    }
}
