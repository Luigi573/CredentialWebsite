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
    }
}
