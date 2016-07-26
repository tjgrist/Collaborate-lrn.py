using Collaborate_lrn_Py.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class EducatorViewModel
    {
        public List<Tutorial> EducatorTutorials { get; set; }

        public List<CollaborativeTutorial> AddedCollaboratorsToTheseTutorials { get; set; }
        
        public List<CollaborativeTutorial> CollaborativeTutorials { get; set; }

        public List<Quiz> EducatorQuizzes { get; set; }

        [Display(Name = "Educator Score")]
        public int Points { get; set; }

        [Display(Name = "Popular Tutorials")]
        public ICollection<Tutorial> PopularTutorials { get; set; }

    }
}