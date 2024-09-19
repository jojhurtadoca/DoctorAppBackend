using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 100 characters")]
        public string Password { get; set; }
    }
}
