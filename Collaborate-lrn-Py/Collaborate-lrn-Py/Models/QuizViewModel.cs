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
        [Display(Name = "Tutorials")]
        public Tutorial TutorialSelection { get; set; }

        public string ExpectedOutput { get; set; }
    }
}