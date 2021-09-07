using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure;

namespace Web.Pages.Mitarbeiter
{
    public class DetailsModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public DetailsModel(Infrastructure.DomainDbContext context)
        {
            _context = context;
        }

        public Domain.Models.Mitarbeiter Mitarbeiter { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mitarbeiter = await _context.Mitarbeiter.FirstOrDefaultAsync(m => m.Id == id);

            if (Mitarbeiter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
