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

        [Display(Name = "Code for the Student to Edit")]
        public string DisplayedCode { get; set; }

        [Display(Name = "Expected output of the student's code be?")]
        public string ExpectedOutput { get; set; }

        [Display(Name = "Example Answer")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Tutorial Name")]
        public string TutorialSelection { get; set; }
    }
}