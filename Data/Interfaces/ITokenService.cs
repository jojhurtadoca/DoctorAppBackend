using Models.Entities;

namespace Data.Interfaces
{
    public interface ITokenService
    {
        Task<string> MakeToken(AppUser user);
    }
}
