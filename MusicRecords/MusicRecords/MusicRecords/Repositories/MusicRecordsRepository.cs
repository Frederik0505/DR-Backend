using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MusicRecords.Data;
using MusicRecords.Models;

namespace MusicRecords.Repositories;

public class MusicRecordsRepository : IMusicRecordsRepository
{
    private readonly MusicRecordsDbContext _context;

    public MusicRecordsRepository(MusicRecordsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<MusicRecord> GetAll(string? title = null, string? artist = null)
    {
        IQueryable<MusicRecord> query = _context.MusicRecords.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(r => r.Title.Contains(title));
        if (!string.IsNullOrWhiteSpace(artist))
            query = query.Where(r => r.Artist.Contains(artist));

        return query.OrderBy(r => r.Id).ToList();
    }

    public MusicRecord? GetById(int id)
        => _context.MusicRecords.AsNoTracking().FirstOrDefault(r => r.Id == id);

    public MusicRecord? Add(MusicRecord record)
    {
        if (record is null || !record.IsValid())
            return null;

        record.Id = 0; // Databasen giver Id via IDENTITY
        _context.MusicRecords.Add(record);
        _context.SaveChanges();
        return record; // Id er nu udfyldt af databasen
    }

    
    public MusicRecord? Update(int id, MusicRecord record)
    {
        Validate(id, record);

        var existing = _context.MusicRecords.Find(id);
        if (existing is null)
            return null;

        existing.Title = record.Title.Trim();
        existing.Artist = record.Artist.Trim();
        existing.Duration = record.Duration;
        existing.PublicationYear = record.PublicationYear;
        _context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var record = _context.MusicRecords.Find(id);
        if (record is null)
            return false;

        _context.MusicRecords.Remove(record);
        _context.SaveChanges();
        return true;
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
}