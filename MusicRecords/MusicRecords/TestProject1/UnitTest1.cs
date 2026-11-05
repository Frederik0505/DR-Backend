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
            var result = _musicRecordsRepository.GetAll(
                title: "Abbey Road",
                artist: "The Beatles");

            Assert.Single(result);
            Assert.Equal("Abbey Road", result.First().Title);
            Assert.Equal("The Beatles", result.First().Artist);
        }

        // US5: Test at en plade kan slettes
        [Fact]
        public void Delete_ExistingRecord_ReturnsTrue()
        {
            // Sletter pladen med ID 1
            var result = _musicRecordsRepository.Delete(1);

            // Kontrollerer at sletningen lykkedes
            Assert.True(result);

            // Kontrollerer at der nu kun er 2 plader
            Assert.Equal(2, _musicRecordsRepository.GetAll().Count());

            // Kontrollerer at pladen med ID 1 er væk
            Assert.DoesNotContain(
                _musicRecordsRepository.GetAll(),
                record => record.Id == 1);
        }

        // US5: Test at et ukendt ID ikke kan slettes
        [Fact]
        public void Delete_NonExistingRecord_ReturnsFalse()
        {
            // Forsøger at slette en plade der ikke findes
            var result = _musicRecordsRepository.Delete(999);

            // Kontrollerer at sletningen ikke lykkedes
            Assert.False(result);

            // Kontrollerer at de 3 oprindelige plader stadig findes
            Assert.Equal(3, _musicRecordsRepository.GetAll().Count());
        }
    }
}

