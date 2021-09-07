using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure;

namespace Web.Pages.Begehungsobjekte
{
    public class IndexModel : PageModel
    {
        private readonly Infrastructure.DomainDbContext _context;

        public IndexModel(Infrastructure.DomainDbContext context)
        {
            _context = context;
        }

        public IList<Begehungsobjekt> Begehungsobjekt { get;set; }

        public async Task OnGetAsync()
        {
            Begehungsobjekt = await _context.Begehungsobjekt.ToListAsync();
        }
    }
}
