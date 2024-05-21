using Infrastructure.Enums;

namespace Project.Application.Reservations.Models
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int OfferId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatusEnum Status { get; set; }
    }
}