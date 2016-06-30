using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class TutorialViewModel
    {
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Difficulty { get; set; }
        [Required]
        [Display(Name = "Instructions")]
        public ICollection<string> BodyText { get; set; }

        //consider adding a simple function that returns the user that creates the tutorial.

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}