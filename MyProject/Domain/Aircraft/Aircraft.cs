namespace MyProject.Domain.Aircraft
{
    public class Aircraft : EntityBase<int>
    {
        public string Model { get; set; }
        public int TotalSeats { get; set; }
    }
}
