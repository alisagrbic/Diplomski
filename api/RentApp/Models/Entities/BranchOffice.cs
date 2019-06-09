using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class BranchOffice
    {
        public int BranchOfficeID { get; set; }
      //  [Required]
        public string Address { get; set; }
        //[Required]
        public float Latitude { get; set; }
        //[Required]
        public float Longitude { get; set; }
       // [Required]
        public string Image { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceID { get; set; }

        [ForeignKey("AppUser")]
        public int CreatorID { get; set; }
        public bool Deleted { get; set; }

        public Service ServiceId { get; set; }
        public AppUser AppUser { get; set; }
    }
}