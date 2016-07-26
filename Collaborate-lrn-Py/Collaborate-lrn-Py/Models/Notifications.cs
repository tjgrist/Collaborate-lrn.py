using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collaborate_lrn_Py.Models
{
    public class Notifications
    {
        public Notifications()
        {
            this.NotificationsList = new List<Comment>();
        }
        public int Id { get; set; }
        public ICollection<Comment> NotificationsList { get; set; }
    }
}