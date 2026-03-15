using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserInfoModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<Teacher> _userManager;
        public Teacher Teacher { get; set; } = default!;

        public UserInfoModel(AppDbContext appDbContext, UserManager<Teacher> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id != null)
            {
                var teacher = await _appDbContext.Users.Include(t => t.School).FirstOrDefaultAsync(t => t.Id == id);

                if (teacher != null)
                {
                    Teacher = teacher;
                    return Page();
                }
            } 

            return NotFound();
        }
    }
}
