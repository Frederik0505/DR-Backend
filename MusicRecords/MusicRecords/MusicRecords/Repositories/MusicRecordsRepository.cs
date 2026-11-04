using MusicRecords.Models;

namespace MusicRecords.Repositories
{
    public class MusicRecordsRepository : IMusicRecordsRepository
    {
        private readonly List<MusicRecord> _musicRecords;

        public MusicRecordsRepository()
        {
            _musicRecords = new List<MusicRecord>
            {
                new MusicRecord {Id=1, Title = "Abbey Road", Artist = "The Beatles", Duration = 2847, PublicationYear = 1969},
                new MusicRecord {Id=2, Title = "Thriller", Artist = "Michael Jackson", Duration = 2533, PublicationYear = 1982},
                new MusicRecord {Id=3, Title = "Parachutes", Artist = "Coldplay", Duration = 2482, PublicationYear = 2000}
            };
        }

        public IEnumerable<MusicRecord> GetAll()
        {
            return new List<MusicRecord>(_musicRecords);
        }

        public MusicRecord? Add(MusicRecord record)
        {
            if (record == null)
            {
                return null;
            }

            if (!record.IsValid())
            {
                return null;
            }

            int newId = 1;

            foreach (MusicRecord musicRecord in _musicRecords)
            {
                if (musicRecord.Id >= newId)
                {
                    newId = musicRecord.Id + 1;
                }
            }

            record.Id = newId;
            _musicRecords.Add(record);

            return record;
        }
    }
}
