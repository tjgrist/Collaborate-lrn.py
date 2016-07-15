using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class EducatorViewModel
    {
        public List<Tutorial> EducatorTutorials { get; set; }

        public List<Quiz> EducatorQuizzes { get; set; }

        [Display(Name = "Educator Score")]
        public int Points { get; set; }
    }
}