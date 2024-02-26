using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Postbank.Models
{
    public class BankTransactionDto
    {
        public int Id { get; set; }
        [DisplayName("Van")]
        public string From { get; set; }
        [DisplayName("Naar")]
        public string To { get; set; }
        [DisplayName("Bedrag"), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [DisplayName("Omschrijving")]
        public string? Description { get; set; }
        [DisplayName("Datum"), DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}
