using System.ComponentModel;

namespace Postbank.Models
{
    public class BankTransferDto
    {
        [DisplayName("Naar")]
        public string To { get; set; }
        [DisplayName("Bedrag")]
        public decimal Amount { get; set; }
        [DisplayName("Omschrijving")]
        public string? Description { get; set; }
    }
}
