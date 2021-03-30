using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyUniversityApp.Data;
using MyUniversityApp.Models;

namespace MyUniversityApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly MyUniversityApp.Data.SchoolContext _context;
        private readonly MvcOptions _mvcOptions;


        public IndexModel(MyUniversityApp.Data.SchoolContext context, IOptions<MvcOptions> mvcOptions)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students
                .Take(_mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
        }
    }
}
