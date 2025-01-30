using System.ComponentModel.DataAnnotations;

namespace JointPresentation.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Nickname")]
        public string UserName { get; set; }
    }
}
