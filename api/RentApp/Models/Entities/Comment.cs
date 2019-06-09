using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        [ForeignKey("Service")]
        public int ServiceID { get; set; }      
        public string Text { get; set; }
        public bool Deleted { get; set; }



        public AppUser Client { get; set; }
        public Service Service { get; set; }
    }
}