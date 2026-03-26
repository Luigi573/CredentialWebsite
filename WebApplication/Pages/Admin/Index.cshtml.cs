using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly AppDbContext _context;
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        public SchoolYear CurrentSchoolYear { get; set; } = new SchoolYear();

        public ManageUsersModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Teachers = await _context.Users.Include(teacher => teacher.School).ToListAsync();
            CurrentSchoolYear = _context.SchoolYears.Where(sy => sy.IsActive).FirstOrDefault() ?? new SchoolYear();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string? id)
        {
            if (id != null)
            {
                var teacher = await _context.Users.FindAsync(id);

                if (teacher == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(teacher);
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAdvanceYearAsync()
        {
            if(CurrentSchoolYear.StartDate <= DateTime.Now && CurrentSchoolYear.EndDate >= DateTime.Now)
            {
                //Update students
                var students = await _context.Students.ToListAsync();

                foreach (var student in students)
                {
                    if ((student.Semester + 2) < 6)
                    {
                        student.Semester += 2;
                    }
                    else
                    {
                        student.IsActive = false;
                    }
                }

                var oldYear = await _context.SchoolYears.Where(sy => sy.IsActive).FirstOrDefaultAsync();
                oldYear?.IsActive = false;

                SchoolYear newYear = new SchoolYear
                {
                    Name = $"{DateTime.Now.Year}-{DateTime.Now.Year + 1}",
                    StartDate = new DateTime(DateTime.Now.Year, 8, 15),
                    EndDate = new DateTime(DateTime.Now.Year + 1, 1, 5),
                    IsActive = true
                };

                _context.SchoolYears.Add(newYear);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }

            return Forbid();
        }
    }
}
