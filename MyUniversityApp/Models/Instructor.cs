﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniversityApp.Models
{
    public class Instructor
    {

        public int ID { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name ="Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public ICollection<Course> Courses { get; set; }
        public OfficeAssignment OfficeAssignmnet { get; set; }
    }
}
