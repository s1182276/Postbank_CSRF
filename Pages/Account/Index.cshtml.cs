using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Postbank.Data;
using Postbank.Models;

namespace Postbank.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly Postbank.Data.PostbankContext _context;

        public IndexModel(Postbank.Data.PostbankContext context)
        {
            _context = context;
        }

        public BankUser BankUser { get; set; } = default!;
        public BankAccount BankAccount { get; set; } = default!;

        public IEnumerable<BankTransactionDto> BankTransactions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var sessionToken = Request.Cookies["PostBankUserSessionToken"];

            if (sessionToken == null) { return RedirectToPage("../Index");}

            var user = await _context.BankUsers.FirstOrDefaultAsync(u => u.SessionToken == sessionToken);

            if (user == null) { return NotFound(); }

            var bankaccount = await _context.BankAccounts.FirstOrDefaultAsync(a => a.BankUserId == user.Id);

            if (bankaccount == null)
            {
                return NotFound();
            }
            else
            {
                BankUser = user;
                BankAccount = bankaccount;
                BankTransactions = await _context.BankTransactions
                            .Where(u => u.ToId == user.Id || u.FromId == user.Id)
                            .Select(b => new BankTransactionDto()
                            {
                                Id = b.Id,
                                Amount = b.Amount,
                                From = _context.BankUsers
                                            .Where(c => c.Id == b.FromId)
                                            .Select(c => c.Name)
                                            .SingleOrDefault(),
                                To = _context.BankUsers
                                            .Where(c => c.Id == b.ToId)
                                            .Select(c => c.Name)
                                            .SingleOrDefault(),
                                Description = b.Description,
                                DateCreated = b.DateCreated,
                            })
                            .ToListAsync();
            }
            return Page();
        }
    }
}
