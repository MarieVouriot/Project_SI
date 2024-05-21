using Infrastructure.Enums;

namespace Infrastructure.Entities
{
    public class Housing
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public HousingTypeEnum Type { get; set; }

        // Navigation properties
        public User Owner { get; set; }

        // Reverse navigation
        public ICollection<Offer> Offers { get; set; }
    }
}
