using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IMedicalRecordRepository: IGenericRepository<MedicalRecord>
    {
        void Update(MedicalRecord MedicalRecord);
    }
}
