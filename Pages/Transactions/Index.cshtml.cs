using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Postbank.Data;
using Postbank.Models;

namespace Postbank.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly PostbankContext _context;

        public IndexModel(PostbankContext context)
        {
            _context = context;
        }

        public IList<BankTransactionDto> BankTransaction { get; set; } = default!;

        public async Task OnGetAsync()
        {
            BankTransaction = await _context.BankTransactions
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
    }
}
