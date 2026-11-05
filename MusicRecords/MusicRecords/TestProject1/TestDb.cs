using Microsoft.EntityFrameworkCore;
using MusicRecords.Data;
using MusicRecords.Models;

namespace TestProject1;

public static class TestDb
{
    // Ny tom database i hukommelsen til hver test, med de samme 3 plader som før
    public static MusicRecordsDbContext Create()
    {
        var options = new DbContextOptionsBuilder<MusicRecordsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new MusicRecordsDbContext(options);
        context.MusicRecords.AddRange(
            new MusicRecord { Id = 1, Title = "Abbey Road", Artist = "The Beatles", Duration = 2847, PublicationYear = 1969 },
            new MusicRecord { Id = 2, Title = "Thriller", Artist = "Michael Jackson", Duration = 2533, PublicationYear = 1982 },
            new MusicRecord { Id = 3, Title = "Parachutes", Artist = "Coldplay", Duration = 2482, PublicationYear = 2000 });
        context.SaveChanges();
        return context;
    }
}