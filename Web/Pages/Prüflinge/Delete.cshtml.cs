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
    public class DeleteModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public DeleteModel(Infrastructure.DomainDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Prüfling = await _context.Prüfling.FindAsync(id);

            if (Prüfling != null)
            {
                _context.Prüfling.Remove(Prüfling);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
