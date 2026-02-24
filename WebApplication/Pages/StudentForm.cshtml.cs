using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;

namespace WebApplication.Pages
{
    public class StudentFormModel : PageModel
    {
        private readonly WebApplication.Data.AppDbContext _context;

        public StudentFormModel(WebApplication.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(Student);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
