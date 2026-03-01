using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;

namespace WebApplication.Pages
{
    public class StudentFormModel : PageModel
    {
        private readonly AppDbContext _context;

        public StudentFormModel(AppDbContext context)
        {
            _context = context;
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
            else
            {
                return Page();
            }
        }
    }
}
