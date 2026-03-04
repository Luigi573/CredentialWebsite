using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebApplication.Data.AppDbContext _context;

        public EditModel(WebApplication.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        [BindProperty]
        public IFormFile? UploadedPhoto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =  await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            Student = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var student = await _context.Students.FindAsync(Student.Id);
            if (student != null)
            {
                student.Name = Student.Name;
                student.FamilyName = Student.FamilyName;
                student.BloodType = Student.BloodType;
                student.CURP = Student.CURP;
                student.TutorName = Student.TutorName;
                student.TutorPhone = Student.TutorPhone;
                student.SchoolPeriod = Student.SchoolPeriod;
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
    }
}
