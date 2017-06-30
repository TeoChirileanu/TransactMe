using System.ComponentModel.DataAnnotations;

namespace TransactMe.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}