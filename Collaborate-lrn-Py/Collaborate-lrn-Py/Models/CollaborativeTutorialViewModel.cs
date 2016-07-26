using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class CollaborativeTutorialViewModel
    {
        public CollaborativeTutorial Tutorial { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}