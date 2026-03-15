using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;

namespace WebApplication.Pages
{
    [Authorize(Roles = "Admin,Teacher")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;

        public CreateModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                var teacher = await _userManager.GetUserAsync(User);

                if (teacher != null)
                {
                    Student.SchoolId = teacher.SchoolId;
                    _context.Students.Add(Student);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }

                return Forbid();
            }

            return Page();
        }
    }
}
