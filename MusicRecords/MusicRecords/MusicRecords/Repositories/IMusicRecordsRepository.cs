using MusicRecords.Models;

namespace MusicRecords.Repositories;

public interface IMusicRecordsRepository
{
    IEnumerable<MusicRecord> GetAll(string? title = null, string? artist = null);
    MusicRecord? GetById(int id);
    MusicRecord? Update(int id, MusicRecord record);
}
