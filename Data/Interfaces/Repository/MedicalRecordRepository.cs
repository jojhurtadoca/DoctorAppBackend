using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Interfaces.Repository
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            var beforeUpdate = _context.MedicalRecords.FirstOrDefault(e => e.Id == medicalRecord.Id);

            if (beforeUpdate != null)
            {
                beforeUpdate.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
