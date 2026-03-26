using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages
{
    [Authorize(Roles = "Admin,Teacher")]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;

        public EditModel(AppDbContext context, UserManager<Teacher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        [BindProperty]
        public IFormFile? UploadedPhoto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

                if (student != null)
                {
                    Student = student;
                    return Page(); 
                }

                return NotFound();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Students.FindAsync(Student.Id);
                if (student != null)
                {
                    student.Name = Student.Name;
                    student.FamilyName = Student.FamilyName;
                    student.BloodType = Student.BloodType;
                    student.CURP = Student.CURP;
                    student.TutorName = Student.TutorName;
                    student.TutorPhone = Student.TutorPhone;
                    student.Semester = Student.Semester;


                    if (UploadedPhoto != null)
                    {
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos/upload");
                        Directory.CreateDirectory(uploadPath);

                        //Delete existing photo if exists
                        if (!string.IsNullOrEmpty(student.ProfilePictureUrl))
                        {
                            var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", student.ProfilePictureUrl.TrimStart('/'));
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }

                        }

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(UploadedPhoto.FileName)}";
                        var filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await UploadedPhoto.CopyToAsync(stream);
                        }

                        student.ProfilePictureUrl = $"/photos/upload/{fileName}";
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                else
                {
                    return NotFound();
                }
            }

            return Page();
        }
    }
}
