using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Interfaces.Repository
{
    public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Specialty specialty)
        {
            var beforeUpdate = _context.Specialties.FirstOrDefault(e => e.Id == specialty.Id);

            if (beforeUpdate != null)
            {
                beforeUpdate.Name = specialty.Name;
                beforeUpdate.Description = specialty.Description;
                beforeUpdate.Status = specialty.Status;
                beforeUpdate.CreatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
