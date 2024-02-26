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
    public class IndexModel : PageModel
    {
        private readonly Postbank.Data.PostbankContext _context;

        public IndexModel(Postbank.Data.PostbankContext context)
        {
            _context = context;
        }

        public IList<BankUser> BankUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BankUser = await _context.BankUsers.ToListAsync();
        }
    }
}
