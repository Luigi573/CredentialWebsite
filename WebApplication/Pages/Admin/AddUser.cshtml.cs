using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication.Data;

namespace WebApplication.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;

        public IList<SelectListItem> Schools { get; set; } = default!;

        public AddUserModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            PopulateSchools();
        }

        [BindProperty]
        public Teacher Teacher { get; set; } = default!;
        [BindProperty][Required][DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo minúsculas, mayúsculas, números y caracteres especiales")]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Teacher.UserName = Teacher.Email;

            var result = await _userManager.CreateAsync(Teacher, Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(Teacher, "Teacher");
                return RedirectToPage("./Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                PopulateSchools();
                return Page();
            }
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
