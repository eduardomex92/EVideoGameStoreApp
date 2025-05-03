using System.ComponentModel.DataAnnotations;

namespace EVideoGameStoreApp.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public String EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
