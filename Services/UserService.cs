using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserDbContext _context;

    public UserService(IUserDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.DeletedAt == null, cancellationToken);
        return user;
    }

    public async Task<User> CreateUserAsync(string firstName, string lastName, string description, string phone, string password, CancellationToken cancellationToken)
    {
        string hashedPassword;
        using (var hmac = new HMACSHA512())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = hmac.ComputeHash(passwordBytes);
            
            hashedPassword = Convert.ToBase64String(hashBytes);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Firstname = firstName,
            Lastname = lastName,
            Description = description,
            PhoneNumber = phone,
            PasswordHash = hashedPassword,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return user;
    }

    public async Task UpdateUserAsync(Guid userId, string firstName, string lastName, string description, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(userId, cancellationToken);
        if (user == null) throw new Exception("User not found");
        
        user.Firstname = firstName;
        user.Lastname = lastName;
        user.Description = description;
        user.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsPhoneNumberFreeAsync(string phone, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone, cancellationToken);
        return user == null;
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(userId, cancellationToken);
        if (user == null) throw new Exception("User not found");
        
        user.DeletedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}