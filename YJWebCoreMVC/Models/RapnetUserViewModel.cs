// venkat 12/04/2025 added
using System.ComponentModel.DataAnnotations;

namespace YJWebCoreMVC.Models
{
    public class RapnetUserViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
