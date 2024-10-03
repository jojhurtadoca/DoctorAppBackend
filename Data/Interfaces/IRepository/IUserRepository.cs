using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IUserRepository: IGenericRepository<AppUser>
    {
        public Task<List<AppUser>> GetUsersWithRoles();
    }
}
