using System.Domain.Entities;

namespace System.Domain.Services.Contract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
