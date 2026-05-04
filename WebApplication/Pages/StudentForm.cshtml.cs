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
    public class StudentFormModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;
        private readonly IWebHostEnvironment _environment;
        public List<SelectListItem> Schools { get; set; } = new List<SelectListItem>();

        public StudentFormModel(AppDbContext context, UserManager<Teacher> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            PopulateSchools();
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        [BindProperty]
        public IFormFile? UploadedPhoto { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var teacher = await _userManager.GetUserAsync(User);

                if (teacher != null)
                {
                    var currentSchoolYear = await _context.SchoolYears.FirstOrDefaultAsync(sy => sy.IsActive);
                    Student.SchoolYearId = currentSchoolYear?.Id ?? 0;

                    if (!User.IsInRole("Admin"))
                    {
                        Student.SchoolId = teacher.SchoolId;
                    }

                    if (UploadedPhoto != null)
                    {
                        var uploadPath = Path.Combine(_environment.WebRootPath, "photos", "upload");
                        Directory.CreateDirectory(uploadPath);

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(UploadedPhoto.FileName)}";
                        var filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await UploadedPhoto.CopyToAsync(stream);
                        }

                        Student.ProfilePictureUrl = $"/photos/upload/{fileName}";
                    }

                    _context.Students.Add(Student);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }

                return Forbid();
            }

            PopulateSchools();
            return Page();
        }

        private void PopulateSchools()
        {
            var schools = _context.Schools.ToList();
            Schools = schools.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
        }
    }
}
