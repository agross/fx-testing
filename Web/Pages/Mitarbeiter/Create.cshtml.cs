using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;
using Infrastructure;

namespace Web.Pages.Mitarbeiter
{
    public class CreateModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public CreateModel(Infrastructure.DomainDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Domain.Models.Mitarbeiter Mitarbeiter { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mitarbeiter.Add(Mitarbeiter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
