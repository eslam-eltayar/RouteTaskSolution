using System.Domain.Entities;

namespace OrderManagmentSystem.APIs.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public Roles Role { get; set; }

        public string Token { get; set; }
    }
}
