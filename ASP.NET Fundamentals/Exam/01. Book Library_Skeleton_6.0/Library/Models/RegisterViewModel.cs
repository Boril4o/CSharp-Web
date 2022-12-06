using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(60)]
        [MinLength(10)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
