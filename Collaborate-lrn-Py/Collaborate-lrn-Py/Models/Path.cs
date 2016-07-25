using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class Path
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Path")]
        public string PathName { get; set; }

        public bool Published { get; set; }
        public int Votes { get; set; }

        public virtual ICollection<Tutorial> Tutorials { get; set; }

        [ForeignKey("Creator")]
        public virtual string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

    }
}