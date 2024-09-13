using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
