using MyProject.Domain.Bookings;

namespace MyProject.Domain.Users
{
    public enum Role
    {
        Customer,
        Admin
    }
    public class User : EntityBase<int>
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role UserRole { get; set; }

        // Navigation Property
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
