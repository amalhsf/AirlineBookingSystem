namespace MyProject.Domain.User
{
    public class User : EntityBase<int>
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public enum Role
        {
            Customer,
            Admin
        }
    }
}
