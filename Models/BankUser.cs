using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Postbank.Models
{
    public class BankUser
    {
        public int Id { get; set; }
        [Required, DisplayName("Naam")]
        public string Name { get; set; }
        [Required, EmailAddress, DisplayName("E-mailadres")]
        public string Email { get; set; }
        [DisplayName("Wachtwoord"), MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
        public string SessionToken { get; set; } = Guid.NewGuid().ToString("N");

        public BankAccount BankAccount { get; set; }
    }
}
