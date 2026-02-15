namespace MyProject.Domain.Airport
{
    public class Airport : EntityBase<int>
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
