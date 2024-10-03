using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(40)]
        public string Phone { get; set; }

        [Required]
        public char Gender { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public MedicalRecord MedicalRecord { get; set; }

        [Display(Name = "Created by")]
        public int? CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public AppUser CreatedBy { get; set; }

        [Display(Name = "Updated by")]
        public int? UpdatedById { get; set; }

        [ForeignKey("UpdatedById")]
        public AppUser UpdatedBy { get; set; }

    }
}
