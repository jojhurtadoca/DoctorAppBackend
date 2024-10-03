using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Initializer
{
    public class DbInitializer : IdbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                } 
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            if (_context.Roles.Any(r => r.Name != "Admin"))
            {
                _roleManager.CreateAsync(new AppRole
                {
                    Name = "Admin",
                }).GetAwaiter().GetResult();

                _roleManager.CreateAsync(new AppRole
                {
                    Name = "Agent"
                }).GetAwaiter().GetResult();

                _roleManager.CreateAsync(new AppRole
                {
                    Name = "Doctor"
                }).GetAwaiter().GetResult();
            }

            AppUser adminUser = _context.AppUsers.Where(r => r.UserName == "Administrator").FirstOrDefault();

            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = "Administrator",
                    Email = "admin@doctorapp.com",
                    Lastname = "Doe",
                    Name = "John"
                };

                _userManager.CreateAsync(user, "Admin123").GetAwaiter().GetResult();
            }

            // We need to check if Administrator already has Admin role
            AppRole adminRole = _context.Roles.Where(r => r.Name == "Admin").FirstOrDefault();
            AppUserRoles role = _context.UserRoles.Where(r => r.RoleId == adminRole.Id && r.UserId == adminUser.Id).FirstOrDefault();

            if (role == null)
            {
                _userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
        }
    }
}
