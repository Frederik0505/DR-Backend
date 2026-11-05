using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public interface IMusicRecordsRepository
    {
        // Henter alle plader og giver mulighed for filtrering
        public IEnumerable<MusicRecord> GetAll(string? title = null, string? artist = null);

        // Sletter en plade ud fra dens ID
        public bool Delete(int id);
    }
}
