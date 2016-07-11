using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class CollaborateViewModel
    {
        [Display(Name = "Tutorial")]
        public string TutorialSelection { get; set; }
        public Tutorial Tutorial { get; set; }
        [Display(Name = "Collaborator:")]
        public string InputCollaborator { get; set; }
        public ApplicationUser User { get; set; }
    }
}