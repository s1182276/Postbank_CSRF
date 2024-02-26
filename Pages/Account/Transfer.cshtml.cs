using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Postbank.Data;
using Postbank.Models;

namespace Postbank.Pages.Account
{
    // Oooh, dangerous! 
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class TransferModel : PageModel
    {
        private readonly PostbankContext _context;

        public TransferModel(PostbankContext context)
        {
            _context = context;
        }

        public BankUser BankUser { get; set; }
        public BankAccount BankAccount { get; set; }

        [BindProperty]
        public BankTransferDto BankTransferDto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var sessionToken = Request.Cookies["PostBankUserSessionToken"];

            if (sessionToken == null) { return RedirectToPage("../Index"); }

            var user = await _context.BankUsers.Include(a => a.BankAccount).FirstOrDefaultAsync(u => u.SessionToken == sessionToken);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                //HttpContext.Session.SetInt32("BankUserId", user.Id);
                return Page();
            }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Trace.WriteLine("Model not valid!");
                return Page();
            }

            var sessionToken = Request.Cookies["PostBankUserSessionToken"];
            if (sessionToken == null) { return RedirectToPage("../Index"); }

            BankUser from = await _context.BankUsers.Include(a => a.BankAccount).FirstOrDefaultAsync(u => u.SessionToken == sessionToken);
            //BankUser from = await _context.BankUsers.Include(a => a.BankAccount).FirstOrDefaultAsync(u => u.Id == HttpContext.Session.GetInt32("BankUserId"));
            BankUser to = await _context.BankUsers.Include(a => a.BankAccount).Where(i => i.Name.Trim() == BankTransferDto.To).FirstOrDefaultAsync();

            if (to == null) { ViewData["Message"] = "Ontvanger bestaat niet"; return Page(); }

            if (from.BankAccount.Balance <= 0 || (from.BankAccount.Balance - BankTransferDto.Amount) <= 0) { ViewData["Message"] = "Balans is niet toereikend.."; return Page(); }

            BankTransaction bankTransaction = new BankTransaction()
            {
                Amount = BankTransferDto.Amount,
                FromId = from.BankAccount.Id,
                ToId = to.BankAccount.Id,
                Description = BankTransferDto.Description,
                DateCreated = DateTime.UtcNow,
            };

            from.BankAccount.Balance -= BankTransferDto.Amount;
            to.BankAccount.Balance += BankTransferDto.Amount;

            _context.BankTransactions.Add(bankTransaction);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Account/Index");
        }
    }
}
