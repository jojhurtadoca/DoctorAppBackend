using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Interfaces.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Doctor doctor)
        {
            var beforeUpdate = _context.Doctors.FirstOrDefault(e => e.Id == doctor.Id);

            if (beforeUpdate != null)
            {
                beforeUpdate.Name = doctor.Name;
                beforeUpdate.Lastname = doctor.Lastname;
                beforeUpdate.Phone = doctor.Phone;
                beforeUpdate.Gender = doctor.Gender;
                beforeUpdate.SpecialtyId = doctor.SpecialtyId;
                beforeUpdate.Address = doctor.Address;
                beforeUpdate.Status = doctor.Status;
                beforeUpdate.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
