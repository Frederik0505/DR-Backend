using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public interface IMusicRecordsRepository
    {
        public IEnumerable<MusicRecord> GetAll();

        public MusicRecord? Add(MusicRecord record);
    }
}
