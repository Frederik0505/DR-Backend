using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public interface IUserRepository
    {
        User? ValidateUser(string username, string password);
    }
}