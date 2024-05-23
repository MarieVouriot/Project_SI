using Infrastructure.Enums;

namespace UserService.Housings.Models
{
    public class HousingDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public HousingTypeEnum Type { get; set; }
    }
}
