using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Doctor
    {
        [Key]
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

        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Address must be 1 character")]
        public char Gender { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }

        [Required]
        public int SpecialtyId {  get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
