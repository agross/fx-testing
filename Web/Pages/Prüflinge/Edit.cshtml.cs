using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure;

namespace Web.Pages.Prüflinge
{
    public class EditModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public EditModel(Infrastructure.DomainDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Prüfling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrüflingExists(Prüfling.Id))
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

        private bool PrüflingExists(string id)
        {
            return _context.Prüfling.Any(e => e.Id == id);
        }
    }
}
