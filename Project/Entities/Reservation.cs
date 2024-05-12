using Microsoft.EntityFrameworkCore;
using Project.Enums;

namespace Project.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int OfferId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatusEnum Status { get; set; }

        // navigation properties
        public User Tenant { get; set; }
        public Offer Offer { get; set; }
    }
}
