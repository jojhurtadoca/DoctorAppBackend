using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public string Lastname { get; set; }
        public string Name { get; set; }

        public ICollection<AppUserRoles> UserRoles { get; set; }
    }
}
