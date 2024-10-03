using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IMedicalHistoryRepository: IGenericRepository<MedicalHistory>
    {
        void Update(MedicalHistory MedicalHistory);
    }
}
