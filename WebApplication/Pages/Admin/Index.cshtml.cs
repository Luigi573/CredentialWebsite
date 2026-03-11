using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages.Admin
{
    //[Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly AppDbContext _context;
        public IList<Teacher> Teachers { get; set; } = default!;

        public ManageUsersModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Teachers = await _context.Users.Include(teacher => teacher.School).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Users.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            _context.Users.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
