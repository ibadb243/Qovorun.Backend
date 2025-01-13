using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IUserService
{
    public Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    public Task<User> CreateUserAsync(string firstName, string lastName, string description, string phone, string password, CancellationToken cancellationToken);
    public Task UpdateUserAsync(Guid userId, string firstName, string lastName, string description, CancellationToken cancellationToken);
    public Task<bool> IsPhoneNumberFreeAsync(string phone, CancellationToken cancellationToken);
    public Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
}