using System.ComponentModel.DataAnnotations;
namespace webApi_labs.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password", ErrorMessage ="no no dont douch ")]
        public string ConfirmPassword { get; set; }


    }
}
