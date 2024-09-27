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

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Min Length is 6 and Max Length is 60")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Min Length is 6 and Max Length is 100")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int Status { get; set; }
    }
}
