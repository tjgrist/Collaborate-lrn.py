using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class Tutorial
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Difficulty { get; set; }
        [Required]
        public List<string> BodyText { get; set; }
        [Display( Name = "Created:")]
        public DateTime CreationDate { get; set; }
        public float? Rating { get; set; }

        [ForeignKey("Educator")]
        public virtual string EducatorId { get; set; }
        public virtual ApplicationUser Educator { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }


    }
}