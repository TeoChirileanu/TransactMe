using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransactMe.Models.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        [DisplayName("Client's SSN")]
        [RegularExpression("\\d{3}-\\d{2}-\\d{4}", ErrorMessage = "Please respect the following format: XXX-XX-XXXX")]
        public string ClientSsn { get; set; }
    }
}