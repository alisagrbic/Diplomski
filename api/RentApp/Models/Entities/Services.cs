using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
      //  [Required]
        public string Logo { get; set; }
      //  [Required]
        public string Email { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        [ForeignKey("Creator")]
        public int CreatorID { get; set; }
        public bool Deleted { get; set; }

        public AppUser Creator { get; set; }

        public virtual ICollection<BranchOffice> Branches { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }

    }
}