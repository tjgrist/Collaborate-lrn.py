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
        public string Instruction { get; set; }

        public virtual string EducatorId { get; set; }
        [ForeignKey("EducatorId")]
        public virtual ApplicationUser Educator { get; set; }

    }
}