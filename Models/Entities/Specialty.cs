﻿using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Min Length is 6 and Max Length is 60")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Min Length is 6 and Max Length is 100")]

        public string Description { get; set; }

        [Required]
        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
