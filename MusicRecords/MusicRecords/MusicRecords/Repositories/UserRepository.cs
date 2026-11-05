using Microsoft.AspNetCore.Identity;
using MusicRecords.Data;
using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly PasswordHasher<User> Hasher = new();
        private readonly MusicRecordsDbContext _context;

        public UserRepository(MusicRecordsDbContext context)
        {
            _context = context;
        }

        public User? ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return null;

            var result = Hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Failed ? null : user;
        }

        // Kaldes ved opstart i Program.cs: opretter standardbrugere, hvis tabellen er tom
        public static void SeedIfEmpty(MusicRecordsDbContext context)
        {
            if (context.Users.Any())
                return;

            context.Users.AddRange(
                CreateUser("admin", "admin123", "Administrator"),
                CreateUser("bruger", "bruger123", "User"));
            context.SaveChanges();
        }

        private static User CreateUser(string username, string password, params string[] roles)
        {
            var user = new User { Username = username, Roles = roles.ToList() };
            user.PasswordHash = Hasher.HashPassword(user, password);
            return user;
        }
    }
}