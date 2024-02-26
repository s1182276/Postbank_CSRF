using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Postbank.Data;
using Postbank.Models;

namespace Postbank.Pages.Manage
{
    public class EditModel : PageModel
    {
        private readonly Postbank.Data.PostbankContext _context;

        public EditModel(Postbank.Data.PostbankContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BankUser BankUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankuser =  await _context.BankUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (bankuser == null)
            {
                return NotFound();
            }
            BankUser = bankuser;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BankUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankUserExists(BankUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BankUserExists(int id)
        {
            return _context.BankUsers.Any(e => e.Id == id);
        }
    }
}
