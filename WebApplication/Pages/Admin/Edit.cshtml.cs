using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Data;

namespace WebApplication.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;
        [BindProperty]
        public Teacher Teacher { get; set; } = default!;
        public IList<SelectListItem> Schools { get; set; } = default!;

        public EditModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
                
            }

            var teacher = await _context.Users.FindAsync(id);
            
            if(teacher == null)
            {
                return NotFound();
            }

            Teacher = teacher;
            PopulateSchools();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userManager.UpdateAsync(Teacher);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                PopulateSchools();
                return Page();
            }

            return RedirectToPage("./Admin/Index");
        }

        private void PopulateSchools()
        {
            var schools = _context.Schools.ToList();
            Schools = schools.Select(school => new SelectListItem
            {
                Value = school.Id.ToString(),
                Text = school.Name
            }).ToList();
        }
    }
}
