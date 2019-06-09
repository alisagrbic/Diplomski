using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
       // [Required]
        public string Model { get; set; }
     //   [Required]
        public string Producer { get; set; }
       // [Required]
        public int ProductionYear { get; set; }
        public string Description { get; set; }
       // [Required]
        public string Image { get; set; }
        public bool Available { get; set; }

        [ForeignKey("AppUser")]
        public int CreatorID { get; set; }

        public bool Deleted { get; set; }

        public float PriceVehicle { get; set; }

        public VehicleType VehicleType { get; set; }
        public Service Service { get; set; }
        public AppUser AppUser { get; set; }

    }
}