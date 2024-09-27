using Models.DTO;

namespace BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetAll();

        Task<DoctorDTO> Add(DoctorDTO DoctorDTO);

        Task Update(DoctorDTO DoctorDTO);

        Task Remove(int id);
    }
}
