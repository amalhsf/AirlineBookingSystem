namespace MyProject.Domain.Passenger
{
    public class Passenger : EntityBase<int>
    {
        public string Name { get; set; }
        public int BookingId { get; set; }
        public string NationalId { get; set; }
        public int Age { get; set; }


    }
}
