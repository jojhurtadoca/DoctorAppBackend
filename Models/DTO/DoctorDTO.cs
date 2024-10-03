using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Lastname must be between 1 and 60 characters")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 60 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Address must be between 1 and 100 characters")]
        public string Address { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Phone must be between 1 and 40 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Specialty is required")]
        public int SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }

        public int Status { get; set; }
    }
}
