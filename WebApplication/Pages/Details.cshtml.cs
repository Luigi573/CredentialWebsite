using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages
{
    [Authorize(Roles = "Admin,Teacher")]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

                if (student is not null)
                {
                    Student = student;

                    return Page();
                }
            }            

            return NotFound();
        }
    }
}
