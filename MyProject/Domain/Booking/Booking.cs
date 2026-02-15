namespace MyProject.Domain.Booking
{
    public class Booking : EntityBase<int>
    {
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public enum Status
        {
            Pending, 
            Confirmed, 
            Calncelled
        }
    }
}
