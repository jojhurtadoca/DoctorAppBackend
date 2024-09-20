using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SpecialtyDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Min Length is 6 and Max Length is 60")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Min Length is 6 and Max Length is 100")]

        public string Description { get; set; }
        public int Status { get; set; }
    }
}
