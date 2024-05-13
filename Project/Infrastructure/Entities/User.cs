namespace Project.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsOwner { get; set; }

        // Reverse navigation
        public ICollection<Housing> Housings { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
