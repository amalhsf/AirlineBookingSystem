namespace MyProject.Domain.Seat
{
    public class Seat : EntityBase<int>
    {
        public string SeatNumber { get; set; }
        public enum Class
        {
            Economy,
            Business
        }
        public bool IsBooked { get; set; }
    }
}
