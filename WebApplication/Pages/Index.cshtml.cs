using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication.Data.AppDbContext _context;
        public IList<Student> Student { get; set; } = default!;

        public IndexModel(WebApplication.Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}
