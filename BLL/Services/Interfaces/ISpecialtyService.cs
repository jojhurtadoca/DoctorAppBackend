using Models.DTO;

namespace BLL.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<SpecialtyDTO>> GetAll();

        Task<IEnumerable<SpecialtyDTO>> GetActives();

        Task<SpecialtyDTO> Add(SpecialtyDTO specialtyDTO);

        Task Update(SpecialtyDTO specialtyDTO);

        Task Remove(int id);
    }
}
