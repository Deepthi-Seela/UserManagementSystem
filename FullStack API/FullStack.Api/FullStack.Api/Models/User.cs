namespace FullStack.Api.Controllers.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Role { get; set; }

        public Boolean IsActive { get; set; }
    }
}
