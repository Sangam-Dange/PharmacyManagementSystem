namespace PharmacyManagementSystem.Authentication
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string requestedFor { get; set; } = string.Empty;

    }
}
