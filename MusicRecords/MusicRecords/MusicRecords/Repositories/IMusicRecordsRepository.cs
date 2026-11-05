using MusicRecords.Models;

namespace MusicRecords.Repositories
{

  public interface IMusicRecordsRepository
  {
      IEnumerable<MusicRecord> GetAll(string? title = null, string? artist = null);
      MusicRecord? Add(MusicRecord record);
      MusicRecord? GetById(int id);
      MusicRecord? Update(int id, MusicRecord record);
   // Sletter en plade ud fra dens ID
      bool Delete(int id);
  }
}
