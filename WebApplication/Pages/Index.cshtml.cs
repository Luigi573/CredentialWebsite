using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        [BindProperty]
        public IList<int> SelectedStudents { get; set; } = new List<int>();

        public IndexModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var teacher = await _userManager.GetUserAsync(User);

            if (teacher != null)
            {
                Students = await _context.Students.Where(s => s.SchoolId == teacher.SchoolId && s.IsActive).ToListAsync();
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
    }
}
