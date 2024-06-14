
using CodeFirst.Models;
using CodeFirst.Models.Authorization;

namespace CodeFirst.Interfaces;

public interface IClientRepository
{
    Task<RegisterRequest?> RegisterClientAsync(RegisterRequest registerRequest);
    Task<LoginRequest?> Login(LoginRequest loginRequest);
}