using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransactMe.Models.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        [DisplayName("Client's First Name")]
        [RegularExpression("[A-Z][a-z][a-z]+", ErrorMessage =
            "Please enter simple names like Theo, John, Michael etc.")]
        public string ClientFirstName { get; set; }

        [Required]
        [DisplayName("Client's Last Name")]
        [RegularExpression("[A-Z][a-z][a-z]+", ErrorMessage =
            "Please enter simple names like Stewart, Jones, Cramer etc.")]
        public string ClientLastName { get; set; }

        [Required]
        [DisplayName("Client's SSN")]
        [RegularExpression("\\d{3}-\\d{2}-\\d{4}", ErrorMessage = "Please respect the following format: XXX-XX-XXXX")]
        public string ClientSsn { get; set; }

        [Required]
        [DisplayName("Type of Transaction")]
        public string TransactionType { get; set; }

        [Required]
        [DisplayName("Currency")]
        public string CurrencyName { get; set; }

        [Required]
        [DisplayName("Amount")]
        [Range(1, 10000, ErrorMessage = "No more than 10.000 units, no less than 1")]
        public int Amount { get; set; } = 10;
    }
}