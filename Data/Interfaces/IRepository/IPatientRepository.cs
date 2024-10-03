using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IPatientRepository: IGenericRepository<Patient>
    {
        void Update(Patient patient);
    }
}
