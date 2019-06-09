using System.ComponentModel.DataAnnotations.Schema;

namespace RentApp.Models.Entities
{
    public class Rate
    {
        public int RateID { get; set; }
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        [ForeignKey("Service")]
        public int ServiceID { get; set; }
        public decimal Value { get; set; }

        public AppUser Client { get; set; }
        public Service Service { get; set; }
    }
}