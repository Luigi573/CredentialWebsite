using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;

namespace WebApplication.Pages
{
    [Authorize(Roles = "Admin,Teacher")]
    public class StudentFormModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;

        public StudentFormModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
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
                    Student.SchoolId = teacher.SchoolId;
                    _context.Students.Add(Student);

                    if (UploadedPhoto != null)
                    {
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos/upload");
                        Directory.CreateDirectory(uploadPath);

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(UploadedPhoto.FileName)}";
                        var filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await UploadedPhoto.CopyToAsync(stream);
                        }

                        Student.ProfilePictureUrl = $"/photos/upload/{fileName}";
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }

                return Forbid();
            }

            return Page();
        }
    }
}
