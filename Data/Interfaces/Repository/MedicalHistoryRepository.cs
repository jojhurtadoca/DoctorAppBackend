using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Interfaces.Repository
{
    public class MedicalHistoryRepository : Repository<MedicalHistory>, IMedicalHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalHistoryRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(MedicalHistory medicalHistory)
        {
            var beforeUpdate = _context.MedicalHistory.FirstOrDefault(e => e.Id == medicalHistory.Id);

            if (beforeUpdate != null)
            {
                beforeUpdate.UpdatedAt = DateTime.Now;
                beforeUpdate.Observation = medicalHistory.Observation;
                _context.SaveChanges();
            }
        }
    }
}
