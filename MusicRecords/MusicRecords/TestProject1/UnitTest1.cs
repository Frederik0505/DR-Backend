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
