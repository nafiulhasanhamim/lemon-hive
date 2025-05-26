using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs
{
    public class LoginWithOTPDTO
    {

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;

    }
}