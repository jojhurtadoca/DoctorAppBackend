using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid MedicalRecordId { get; set; }

        [ForeignKey("MedicalRecordId")]
        public MedicalRecord MedicalRecord { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Observation {  get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
