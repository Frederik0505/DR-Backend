using Microsoft.AspNetCore.Identity;
using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PasswordHasher<User> _hasher = new();
        private readonly List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                CreateUser("admin", "admin123", "Administrator"),
                CreateUser("bruger", "bruger123", "User")
            };
        }

        private User CreateUser(string username, string password, params string[] roles)
        {
            var user = new User { Username = username, Roles = roles.ToList() };
            user.PasswordHash = _hasher.HashPassword(user, password);
            return user;
        }

        public User? ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
                return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Failed ? null : user;
        }
    }
}