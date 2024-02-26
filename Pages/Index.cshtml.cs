using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Postbank.Data;
using Postbank.Models;
using System.Diagnostics;

namespace Postbank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PostbankContext _context;

        public IndexModel(ILogger<IndexModel> logger, PostbankContext context)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid){ return Page(); }

            BankUser user = await _context.BankUsers.FirstOrDefaultAsync(u => u.Email == Email);

            if (user == null) { return Page(); }

            if (user.Password != Password) { return Page(); }

            Trace.WriteLine($"Found user {user.Name}");
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(30),
                Secure = true
            };

            Response.Cookies.Append("PostBankUserSessionToken", user.SessionToken, cookieOptions);

            return RedirectToPage("Account/Index");
        }
    }
}
