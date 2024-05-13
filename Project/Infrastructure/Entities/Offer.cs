using Project.Enums;

namespace Project.Infrastructure.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerDay { get; set; }

        // navigation properties
        public Housing Housing { get; set; }

        // reverse navigation
        public ICollection<Reservation> Reservations { get; set; }
    }
}
