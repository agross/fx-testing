using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure;

namespace Web.Pages.Begehungen
{
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public DeleteModel(Infrastructure.DomainDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Begehung Begehung { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Begehung = await _context.Begehungen.FirstOrDefaultAsync(m => m.Id == id);

            if (Begehung == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Begehung = await _context.Begehungen.FindAsync(id);

            if (Begehung != null)
            {
                _context.Begehungen.Remove(Begehung);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
