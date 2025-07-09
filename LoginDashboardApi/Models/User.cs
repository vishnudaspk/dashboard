namespace LoginDashboardApi.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // In a real app, this would be a hashed password
    }
}
