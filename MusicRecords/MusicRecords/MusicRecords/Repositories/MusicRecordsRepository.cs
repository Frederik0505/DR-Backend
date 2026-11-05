using MusicRecords.Models;

namespace MusicRecords.Repositories;

public class MusicRecordsRepository : IMusicRecordsRepository
{
    private readonly object _sync = new();
    private readonly List<MusicRecord> _musicRecords = new()
    {
        new MusicRecord { Id = 1, Title = "Abbey Road", Artist = "The Beatles", Duration = 2847, PublicationYear = 1969 },
        new MusicRecord { Id = 2, Title = "Thriller", Artist = "Michael Jackson", Duration = 2533, PublicationYear = 1982 },
        new MusicRecord { Id = 3, Title = "Parachutes", Artist = "Coldplay", Duration = 2482, PublicationYear = 2000 }
    };

    public IEnumerable<MusicRecord> GetAll(string? title = null, string? artist = null)
    {
        lock (_sync)
        {
            return _musicRecords
                .Where(r => string.IsNullOrEmpty(title) || r.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Where(r => string.IsNullOrEmpty(artist) || r.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase))
                .Select(Copy)
                .ToList();
        }
    }

    public MusicRecord? GetById(int id)
    {
        lock (_sync)
        {
            var record = _musicRecords.FirstOrDefault(r => r.Id == id);
            return record is null ? null : Copy(record);
        }
    }

    public MusicRecord? Update(int id, MusicRecord record)
    {
        Validate(id, record);

        lock (_sync)
        {
            var existing = _musicRecords.FirstOrDefault(r => r.Id == id);
            if (existing is null)
                return null;

            existing.Title = record.Title.Trim();
            existing.Artist = record.Artist.Trim();
            existing.Duration = record.Duration;
            existing.PublicationYear = record.PublicationYear;
            return Copy(existing);
        }
    }

    private static void Validate(int id, MusicRecord? record)
    {
        if (id <= 0)
            throw new ArgumentException("Id skal være større end 0.");
        if (record is null)
            throw new ArgumentException("En plade skal sendes i request body.");
        if (record.Id != 0 && record.Id != id)
            throw new ArgumentException("Id i pladen skal svare til Id i URL'en.");
        if (string.IsNullOrWhiteSpace(record.Title))
            throw new ArgumentException("Titel må ikke være tom.");
        if (string.IsNullOrWhiteSpace(record.Artist))
            throw new ArgumentException("Kunstner må ikke være tom.");
        if (record.Title.Trim().Length > 200 || record.Artist.Trim().Length > 200)
            throw new ArgumentException("Titel og kunstner må højst være 200 tegn.");
        if (record.Duration <= 0)
            throw new ArgumentException("Varighed skal være større end 0 sekunder.");
        if (record.PublicationYear < 1877 || record.PublicationYear > DateTime.UtcNow.Year + 1)
            throw new ArgumentException("Udgivelsesåret skal være mellem 1877 og næste år.");
    }

    private static MusicRecord Copy(MusicRecord source) => new()
    {
        Id = source.Id,
        Title = source.Title,
        Artist = source.Artist,
        Duration = source.Duration,
        PublicationYear = source.PublicationYear
    };
}
