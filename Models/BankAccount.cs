using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Postbank.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true), DisplayName("Saldo")]
        public decimal Balance { get; set; } = 0;

        public int BankUserId { get; set; }

        [DisplayName("Gebruiker")]
        public BankUser BankUser { get; set; }
    }
}
