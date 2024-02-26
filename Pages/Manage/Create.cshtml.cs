using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Postbank.Data;
using Postbank.Models;

namespace Postbank.Pages.Manage
{
    public class CreateModel : PageModel
    {
        private readonly Postbank.Data.PostbankContext _context;

        public CreateModel(Postbank.Data.PostbankContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BankUser BankUser { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BankUsers.Add(BankUser);
            await _context.SaveChangesAsync();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(30),
                Secure = false
            };

            Response.Cookies.Append("PostBankUserSessionToken", BankUser.SessionToken, cookieOptions);

            return RedirectToPage("../Index");
        }
    }
}
