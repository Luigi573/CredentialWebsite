using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages
{
    [Authorize(Roles = "Admin,Teacher")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;
        public IList<Student> Students { get; set; } = new List<Student>();
        public IList<SelectListItem> Schools { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public IList<int> SelectedStudents { get; set; } = new List<int>();
        [BindProperty]
        public int SelectedSchoolId { get; set; }   

        public IndexModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int? selectedSchool)
        {
            var teacher = await _userManager.GetUserAsync(User);

            if (teacher != null)
            {
                if (User.IsInRole("Admin"))
                {
                    Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
                    PopulateSchools();
                }
                else
                {
                    Students = await _context.Students.Where(s => s.SchoolId == teacher.SchoolId && s.IsActive).ToListAsync();
                }
            }
        }

        public async Task<IActionResult> OnPostDeactivateAsync()
        {
            var teacher = await _userManager.GetUserAsync(User);

            if (teacher != null)
            {
                var studentsToDeactivate = await _context.Students.Where(s => SelectedStudents.Contains(s.Id)).ToListAsync();

                foreach (var student in studentsToDeactivate)
                {
                    student.IsActive = false;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage();
            }

            return Forbid();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            var teacher = await _userManager.GetUserAsync(User);

            if(teacher != null)
            {
                if (User.IsInRole("Admin"))
                {
                    if (SelectedSchoolId > 0)
                    {
                        Students = await _context.Students.Where(s => s.SchoolId == SelectedSchoolId && s.IsActive).ToListAsync();
                    }
                    else
                    {
                        Students = await _context.Students.Where(s => s.IsActive).ToListAsync();
                    }

                    PopulateSchools();
                }
                else
                {
                    Students = await _context.Students.Where(s => s.SchoolId == teacher.SchoolId && s.IsActive).ToListAsync();
                }

                return Page();
            }

            return Forbid();
        }

        public void PopulateSchools()
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
