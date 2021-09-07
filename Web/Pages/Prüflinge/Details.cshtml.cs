using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure;

namespace Web.Pages.Prüflinge
{
    public class DetailsModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public DetailsModel(Infrastructure.DomainDbContext context)
        {
            _context = context;
        }

        public Prüfling Prüfling { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Prüfling = await _context.Prüfling.FirstOrDefaultAsync(m => m.Id == id);

            if (Prüfling == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
