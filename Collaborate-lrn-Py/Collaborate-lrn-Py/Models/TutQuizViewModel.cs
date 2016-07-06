using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class TutQuizViewModel
    {
        //Tutorial fields
        public Tutorial ModelTutorial { get; set; }
        public Quiz ModelQuiz { get; set; }
    }
}