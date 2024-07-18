using System.Domain.Entities;

namespace OrderManagmentSystem.APIs.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Roles Role { get; set; }
    }
}
