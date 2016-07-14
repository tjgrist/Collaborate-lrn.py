using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class GradeViewModel
    {
        public Quiz Quiz { get; set; }
        public string yourcode { get; set; }
        public string output { get; set; }
        public string expected { get; set; }
        public bool JavascriptErrors { get; set; }
    }
}