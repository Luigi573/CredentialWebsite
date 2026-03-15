using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public IList<Student> Students { get; set; } = default!;

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
                Students = await _context.Students.Where(s => s.SchoolId == teacher.SchoolId).ToListAsync();
            }
        }
    }
}
