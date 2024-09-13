using Models.Entities;

namespace Data.Interfaces
{
    public interface ITokenService
    {
        string MakeToken(User user);
    }
}
