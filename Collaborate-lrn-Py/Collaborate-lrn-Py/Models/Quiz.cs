using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Instruction { get; set; }

        [ForeignKey("Educator")]
        public virtual string EducatorId { get; set; }
        public virtual ApplicationUser Educator { get; set; }

        [ForeignKey("Tutorial")]
        public virtual int TutorialId { get; set; }
        public virtual Tutorial Tutorial { get; set; }
    }
}