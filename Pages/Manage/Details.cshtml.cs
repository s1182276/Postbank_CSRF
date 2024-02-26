using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Postbank.Data;
using Postbank.Models;

namespace Postbank.Pages.Manage
{
    public class DetailsModel : PageModel
    {
        private readonly Postbank.Data.PostbankContext _context;

        public DetailsModel(Postbank.Data.PostbankContext context)
        {
            _context = context;
        }

        public BankUser BankUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankuser = await _context.BankUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (bankuser == null)
            {
                return NotFound();
            }
            else
            {
                BankUser = bankuser;
            }
            return Page();
        }
    }
}
