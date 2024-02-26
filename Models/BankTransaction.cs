using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Postbank.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        [DisplayName("Van")]
        public int FromId { get; set; }
        [DisplayName("Naar")]
        public int ToId { get; set; }
        [DisplayName("Bedrag"), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [DisplayName("Omschrijving")]
        public string? Description { get; set; }
        [DisplayName("Datum"), DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}
