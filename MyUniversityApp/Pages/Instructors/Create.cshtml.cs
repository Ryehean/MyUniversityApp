using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyUniversityApp.Data;
using MyUniversityApp.Models;

namespace MyUniversityApp.Pages.Instructors
{
    public class CreateModel : InstructorCoursesPageModel
    {
        private readonly MyUniversityApp.Data.SchoolContext _context;
        private readonly ILogger<InstructorCoursesPageModel> _logger;


        public CreateModel(MyUniversityApp.Data.SchoolContext context,
                    ILogger<InstructorCoursesPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var instructor = new Instructor();
            instructor.Courses = new List<Course>();

            PopulateAssignedCourseData(_context, instructor);
            return Page();
        }

        [BindProperty]
        public Instructor Instructor { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
        {
            var newInstructor = new Instructor();

            if (selectedCourses.Length > 0)
            {
                newInstructor.Courses = new List<Course>();
                _context.Courses.Load();
            }

            //Add selected courses to th new instructor
            foreach (var course in selectedCourses)
            {
                var foundCourse = await _context.Courses.FindAsync(int.Parse(course));
                if (foundCourse != null)
                {
                    newInstructor.Courses.Add(foundCourse);
                }
                else
                {
                    _logger.LogWarning("Course {course} not found",course);
                }
            }

            try
            {
                if (await TryUpdateModelAsync<Instructor>(newInstructor,
                        "Instructor", i => i.FirstName, i => i.LastName,
                                i => i.HireDate, i => i.OfficeAssignmnet))
                {
                    _context.Instructors.Add(newInstructor);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            PopulateAssignedCourseData(_context, Instructor);
            return Page();
        }
    }
}
