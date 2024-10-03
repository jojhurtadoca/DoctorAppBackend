using Data.Interfaces.IRepository;

namespace Data.Interfaces.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ISpecialtyRepository SpecialtyRepository { get; private set; }
        public IDoctorRepository DoctorRepository { get; private set; }
        public IPatientRepository PatientRepository { get; private set; }
        public IMedicalRecordRepository MedicalRecordRepository { get; private set; }
        public IMedicalHistoryRepository MedicalHistoryRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SpecialtyRepository = new SpecialtyRepository(context);
            DoctorRepository = new DoctorRepository(context);
            PatientRepository = new PatientRepository(context);
            MedicalHistoryRepository = new MedicalHistoryRepository(context);
            MedicalRecordRepository = new MedicalRecordRepository(context);
            UserRepository = new UserRepository(context);
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
