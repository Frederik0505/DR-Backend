using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public interface IMusicRecordsRepository
    {

        public IEnumerable<MusicRecord> GetAll(string? title = null, string? artist = null);
    }
}
