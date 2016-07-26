using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class CollaborativeTutorial
    {
        public CollaborativeTutorial()
        {
            this.Collaborators = new List<ApplicationUser>();
            this.Comments = new List<Comment>();
        }
        public int Id { get; set; }

        [ForeignKey("Tutorial")]
        public int TutorialId { get; set; }
        public Tutorial Tutorial { get; set; }

        public virtual ICollection<ApplicationUser> Collaborators { get; set; }

        public virtual List<Comment> Comments { get; set; }

        [Display(Name = "Comment")]
        public string newComment { get; set; }

    }
}