using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class LearningPathModel
    {
        public LearningPathModel()
        {
            this.LearningPaths = new List<Path>();
        }
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Path> LearningPaths { get; set; }
    }
}