using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class DoctorService : IDoctorService
    {

        public readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DoctorDTO> Add(DoctorDTO DoctorDTO)
        {
            try
            {
                Doctor Doctor = new Doctor
                {
                    Name = DoctorDTO.Name,
                    Lastname = DoctorDTO.Lastname,
                    Address = DoctorDTO.Address,
                    Status = DoctorDTO.Status == 1 ? true : false,
                    Phone = DoctorDTO.Phone,
                    Gender = DoctorDTO.Gender,
                    SpecialtyId = DoctorDTO.SpecialtyId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                await unitOfWork.DoctorRepository.Create(Doctor);
                await unitOfWork.Save();

                if (Doctor.Id == 0) 
                {
                    throw new TaskCanceledException("The system couldn't create the entity, please try later");
                }

                return mapper.Map<DoctorDTO>(Doctor);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<IEnumerable<DoctorDTO>> GetAll()
        {
            try
            {
                var list = await unitOfWork.DoctorRepository.GetAll(includeProperties: "Specialty", orderBy: e => e.OrderBy(e => e.Name));
                return mapper.Map<IEnumerable<DoctorDTO>>(list);
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
                var doctor = await unitOfWork.DoctorRepository.GetFirst(e => e.Id == id);
                if (doctor == null)
                {
                    throw new TaskCanceledException("Doctor not found");
                }
                unitOfWork.DoctorRepository.Remove(doctor);
                await unitOfWork.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(DoctorDTO DoctorDTO)
        {
            try
            {
                var Doctor = await unitOfWork.DoctorRepository.GetFirst(e => e.Id == DoctorDTO.Id);
                if (Doctor == null)
                {
                    throw new TaskCanceledException("Doctor not found");
                }
                Doctor.Name = DoctorDTO.Name;
                Doctor.Lastname = DoctorDTO.Lastname;
                Doctor.Phone = DoctorDTO.Phone;
                Doctor.Status = DoctorDTO.Status == 1 ? true : false;
                Doctor.Gender = DoctorDTO.Gender;
                Doctor.Address = DoctorDTO.Address;
                Doctor.SpecialtyId = DoctorDTO.SpecialtyId;

                unitOfWork.DoctorRepository.Update(Doctor);
                await unitOfWork.Save();
            } 
            catch (Exception)
            {
                throw;
            }
        }
    }
}
