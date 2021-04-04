using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyUniversityApp.Models.SchoolViewModels;
using MyUniversityApp.Data;
using MyUniversityApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MyUniversityApp.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SchoolContext _contex;

        public AboutModel(SchoolContext context)
        {
            _contex = context;
        }

        public IList<EnrollmentDateGroup> Students { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _contex.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}
