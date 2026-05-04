using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using WebApplication.Data;

namespace WebApplication.Pages
{
    [Authorize(Roles = "Admin,Teacher")]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;
        private readonly IWebHostEnvironment _environment;
        public List<SelectListItem> Schools { get; set; } = new List<SelectListItem>();

        public EditModel(AppDbContext context, UserManager<Teacher> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
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

                    PopulateSchools();
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

                    if (User.IsInRole("Admin"))
                    {
                        student.SchoolId = Student.SchoolId;
                    }

                    student.ProfilePictureUrl = Student.ProfilePictureUrl;


                    if (UploadedPhoto != null)
                    {
                        var uploadPath = Path.Combine(_environment.WebRootPath, "photos", "upload");
                        Directory.CreateDirectory(uploadPath);

                        //Delete existing photo if exists
                        if (!string.IsNullOrEmpty(student.ProfilePictureUrl))
                        {
                            var existingFilePath = Path.Combine(_environment.WebRootPath, student.ProfilePictureUrl.TrimStart('/'));

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
