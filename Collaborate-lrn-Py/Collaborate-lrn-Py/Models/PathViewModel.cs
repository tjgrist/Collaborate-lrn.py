using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class PathViewModel
    {
        [Display(Name = "Path Selection")]
        public string PathSelection { get; set; }

        public int TutorialId { get; set; }

    }
}