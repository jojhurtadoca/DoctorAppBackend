using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class AppUserRoles: IdentityUserRole<int>
    {
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }
    }
}
