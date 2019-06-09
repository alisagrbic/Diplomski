using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentApp.Models.Entities
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        [Required]
        public DateTime? GetVehicleDate { get; set; }
        [Required]
        public DateTime? ReturnVehicleDate { get; set; }
        [ForeignKey("BranchOffice")]
        [Required]
        public int GetBranchId { get; set; }
        [Required]
        public int ReturnBranchId { get; set; }

        public bool Deleted { get; set; }


        public AppUser Client { get; set; }
        public Vehicle Vehicle { get; set; }
        public BranchOffice BranchOffice { get; set; }
    }
}