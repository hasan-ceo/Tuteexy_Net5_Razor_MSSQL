using System.ComponentModel.DataAnnotations;

namespace Tuteexy.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
