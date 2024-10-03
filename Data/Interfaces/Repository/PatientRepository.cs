using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Interfaces.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Patient patient)
        {
            var beforeUpdate = _context.Patients.FirstOrDefault(e => e.Id == patient.Id);

            if (beforeUpdate != null)
            {
                beforeUpdate.Name = patient.Name;
                beforeUpdate.Lastname = patient.Lastname;
                beforeUpdate.Phone = patient.Phone;
                beforeUpdate.Gender = patient.Gender;
                beforeUpdate.Address = patient.Address;
                beforeUpdate.Status = patient.Status;
                beforeUpdate.UpdatedById = patient.UpdatedById;
                beforeUpdate.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
