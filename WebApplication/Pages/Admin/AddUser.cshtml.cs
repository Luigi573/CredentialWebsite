using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApplication.Data;
using WebApplication.Services;

namespace WebApplication.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;
        private readonly IEmailSender _emailSender;

        public IList<SelectListItem> Schools { get; set; } = new List<SelectListItem>();

        public AddUserModel(AppDbContext context, UserManager<Teacher> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public void OnGet()
        {
            PopulateSchools();
        }

        [BindProperty]
        public Teacher Teacher { get; set; } = default!;
        [Required]
        [BindProperty]
        public string SelectedRole { get; set; } = "Teacher";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateSchools();
                return Page();
            }

            Teacher.UserName = Teacher.Email;
            Teacher.EmailConfirmed = true; 

            var result = await _userManager.CreateAsync(Teacher);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                PopulateSchools();
                return Page();
                
            }

            await _userManager.AddToRoleAsync(Teacher, SelectedRole);

            var token = await _userManager.GeneratePasswordResetTokenAsync(Teacher);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var passwordSetLink = Url.Page("/Account/ResetPassword", pageHandler : null, values: new { area = "Identity", email = Teacher.Email, code = encodedToken }, protocol: Request.Scheme);

            var body = $@"<h1>Tu cuenta ha sido creada.</h1>
                        <p>
                            Por favor, establece tu contraseña utilizando el siguiente enlace: <a href=""{passwordSetLink}"">Establecer contraseña</a>
                        </p>
                        <p>Si el botón no funciona, copia y pega este enlace en tu navegador:</p>
                        <p>{passwordSetLink}</p>";

            await _emailSender.SendEmail(Teacher.Email, "Bienvenido a la plataforma", body);
            return RedirectToPage("./Index");
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
