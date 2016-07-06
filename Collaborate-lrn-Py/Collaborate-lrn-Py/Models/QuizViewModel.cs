using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class QuizViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Instruction { get; set; }
        [Required]
        public string Goal { get; set; }

        [Display(Name = "What should the output of the user's code be?")]
        public string ExpectedOutput { get; set; }

        [Required]
        [Display(Name = "Tutorial Name")]
        public string TutorialSelection { get; set; }
    }
}