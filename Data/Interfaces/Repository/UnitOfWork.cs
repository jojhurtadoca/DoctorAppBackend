using Data.Interfaces.IRepository;

namespace Data.Interfaces.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ISpecialtyRepository SpecialtyRepository { get; private set; }
        public IDoctorRepository DoctorRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SpecialtyRepository = new SpecialtyRepository(context);
            DoctorRepository = new DoctorRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
