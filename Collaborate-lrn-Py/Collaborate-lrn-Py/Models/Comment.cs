using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class Comment
    {
        public Comment()
        {

        }
        public Comment(string text)
        {
            this.Text = text;
            this.Votes = 0;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public int Votes { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}