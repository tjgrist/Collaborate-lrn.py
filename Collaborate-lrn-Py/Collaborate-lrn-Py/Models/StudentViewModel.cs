using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class StudentViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        [Display(Name = "Popular Tutorials")]
        public ICollection<Tutorial> PopularTutorials { get; set; }
    }
}