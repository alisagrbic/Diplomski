using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentApp.Models.Entities
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
      //  [Required]
        public string VehicleName { get; set; }
        public bool Deleted { get; set; }

    }
}