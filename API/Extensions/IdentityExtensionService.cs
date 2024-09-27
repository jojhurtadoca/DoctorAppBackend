using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System.Text;

namespace API.Extensions
{
    public static class IdentityExtensionService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireRole("Admin");
                });
                opt.AddPolicy("AdminAgentPolicy", policy =>
                {
                    policy.RequireRole("Admin", "Agent");
                });
                opt.AddPolicy("DoctorPolicy", policy =>
                {
                    policy.RequireRole("Doctor");
                });
            });
            return services;
        }

    }
}
