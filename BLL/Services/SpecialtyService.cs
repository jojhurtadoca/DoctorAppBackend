using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class SpecialtyService : ISpecialtyService
    {

        public readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SpecialtyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<SpecialtyDTO> Add(SpecialtyDTO specialtyDTO)
        {
            try
            {
                Specialty specialty = new Specialty
                {
                    Name = specialtyDTO.Name,
                    Description = specialtyDTO.Description,
                    CreatedAt = DateTime.Now,
                    Status = specialtyDTO.Status == 1 ? true : false,
                    UpdatedAt = DateTime.Now,
                };
                await unitOfWork.SpecialtyRepository.Create(specialty);
                await unitOfWork.Save();

                if (specialty.Id == 0) 
                {
                    throw new TaskCanceledException("The system couldn't create the entity, please try later");
                }

                return mapper.Map<SpecialtyDTO>(specialty);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<IEnumerable<SpecialtyDTO>> GetAll()
        {
            try
            {
                var list = await unitOfWork.SpecialtyRepository.GetAll(orderBy: e => e.OrderBy(e => e.Name));
                return mapper.Map<IEnumerable<SpecialtyDTO>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var specialty = await unitOfWork.SpecialtyRepository.GetFirst(e => e.Id == id);
                if (specialty == null)
                {
                    throw new TaskCanceledException("Specialty not found");
                }
                unitOfWork.SpecialtyRepository.Remove(specialty);
                await unitOfWork.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(SpecialtyDTO specialtyDTO)
        {
            try
            {
                var specialty = await unitOfWork.SpecialtyRepository.GetFirst(e => e.Id == specialtyDTO.Id);
                if (specialty == null)
                {
                    throw new TaskCanceledException("Specialty not found");
                }
                specialty.Name = specialtyDTO.Name;
                specialty.Description = specialtyDTO.Description;
                specialty.Status = specialtyDTO.Status == 1 ? true : false;
                specialty.UpdatedAt = DateTime.Now;
                unitOfWork.SpecialtyRepository.Update(specialty);
                await unitOfWork.Save();
            } 
            catch (Exception)
            {
                throw;
            }
        }
    }
}
