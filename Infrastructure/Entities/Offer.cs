using Infrastructure.Enums;

namespace Infrastructure.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public int HousingId { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerDay { get; set; }

        // navigation properties
        public Housing Housing { get; set; }

        // reverse navigation
        public ICollection<Reservation> Reservations { get; set; }
    }
}
