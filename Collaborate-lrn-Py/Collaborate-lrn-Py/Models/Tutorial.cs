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

        [Display(Name = "Instructions")]
        public string BodyText { get; set; }

        [Display( Name = "Created:")]
        public DateTime CreationDate { get; set; }

        public float? Rating { get; set; }

        public bool Published { get; set; }


        [ForeignKey("Educator")]
        public virtual string EducatorId { get; set; }
        public virtual ApplicationUser Educator { get; set; }

        [ForeignKey("Quiz")]
        public virtual int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }


    }
}