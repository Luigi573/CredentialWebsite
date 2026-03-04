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
                //student.ProfilePictureUrl = Student.ProfilePictureUrl; TODO: ADD IMAGE UPLOAD FUNCTIONALITY

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
