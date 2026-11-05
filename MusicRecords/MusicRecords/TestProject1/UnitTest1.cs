using MusicRecords.Models;
using MusicRecords.Repositories;

namespace TestProject1
{
    public class MusicRecordsTest
    {
        private readonly MusicRecordsRepository _musicRecordsRepository;

        public MusicRecordsTest()
        {
            _musicRecordsRepository = new MusicRecordsRepository();
        }

        [Fact]
        public void GetAll_ReturnsAllStartData()
        {
            var result = _musicRecordsRepository.GetAll();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Add_ValidRecord()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "25",
                Artist = "Adele",
                Duration = 2900,
                PublicationYear = 2015
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.NotNull(result);
            Assert.Equal(4, _musicRecordsRepository.GetAll().Count());
            Assert.Equal(4, result.Id);
        }

        [Fact]
        public void Add_EmptyTitle()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "",
                Artist = "Adele",
                Duration = 2900,
                PublicationYear = 2015
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.Null(result);
            Assert.Equal(3, _musicRecordsRepository.GetAll().Count());
        }

        [Fact]
        public void Add_EmptyArtist()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "25",
                Artist = "",
                Duration = 2900,
                PublicationYear = 2015
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.Null(result);
        }

        [Fact]
        public void Add_InvalidDuration()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "25",
                Artist = "Adele",
                Duration = 0,
                PublicationYear = 2015
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.Null(result);
        }

        [Fact]
        public void Add_InvalidYear()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "25",
                Artist = "Adele",
                Duration = 2900,
                PublicationYear = 1899
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.Null(result);
        }

        [Fact]
        public void Add_DurationOne()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "Test",
                Artist = "Test Artist",
                Duration = 1,
                PublicationYear = 2020
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.NotNull(result);
        }

        [Fact]
        public void Add_Year1900()
        {
            MusicRecord record = new MusicRecord
            {
                Title = "Test",
                Artist = "Test Artist",
                Duration = 200,
                PublicationYear = 1900
            };

            var result = _musicRecordsRepository.Add(record);

            Assert.NotNull(result);
        public void GetAll_WithTitleFilter_ReturnsFilteredData()
        {
            var result = _musicRecordsRepository.GetAll(title: "Abbey Road");
            Assert.Single(result);
            Assert.Equal("Abbey Road", result.First().Title);
        }

        [Fact]
        public void GetAll_WithArtistFilter_ReturnsFilteredData()
        {
            var result = _musicRecordsRepository.GetAll(artist: "The Beatles");
            Assert.Single(result);
            Assert.Equal("The Beatles", result.First().Artist);
        }

        [Fact]
        public void GetAll_WithBothFilters_ReturnsFilteredData()
        {
            var result = _musicRecordsRepository.GetAll(title: "Abbey Road", artist: "The Beatles");
            Assert.Single(result);
            Assert.Equal("Abbey Road", result.First().Title);
            Assert.Equal("The Beatles", result.First().Artist);
        }
    }
}
