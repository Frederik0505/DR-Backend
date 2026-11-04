namespace MusicRecords.Models
{
    public class MusicRecord
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int PublicationYear { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Artist))
            {
                return false;
            }

            if (Duration <= 0)
            {
                return false;
            }

            if (PublicationYear < 1900 || PublicationYear > DateTime.Now.Year)
            {
                return false;
            }

            return true;
        }
    }
}
