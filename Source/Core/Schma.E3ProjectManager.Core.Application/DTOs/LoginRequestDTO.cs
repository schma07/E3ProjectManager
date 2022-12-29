namespace Schma.E3ProjectManager.Core.Application.DTOs
{
    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } //TODO: Consider removing this?
    }
}
