using MusicRecords.Models;
using Microsoft.AspNetCore.Mvc;

namespace MusicRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicRecordsController : ControllerBase
    {
        private static readonly List<MusicRecord> MusicRecords = new List<MusicRecord>
        {
            new MusicRecord {Id=1, Title = "Abbey Road", Artist = "The Beatles", Duration = 2847, PublicationYear = 1969},
            new MusicRecord {Id=2, Title = "Thriller", Artist = "Michael Jackson", Duration = 2533, PublicationYear = 1982},
            new MusicRecord {Id=3, Title = "Parachutes", Artist = "Coldplay", Duration = 2482, PublicationYear = 2000}
        };

        private static int _nextId = 4;

        // GET: api/MusicRecords
        [HttpGet]
        public IEnumerable<MusicRecord> Get()
        {
            return MusicRecords;
        }

        // GET: api/MusicRecords/5
        [HttpGet]
        [Route("{id}")]
        public MusicRecord? Get(int id)
        {
            return MusicRecords.FirstOrDefault(record => record.Id == id);
        }

        // POST: api/MusicRecords
        [HttpPost]
        public int Post([FromBody] MusicRecord record)
        {
            record.Id = _nextId++;
            MusicRecords.Add(record);
            return record.Id;
        }

        // PUT: api/MusicRecords/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MusicRecord record)
        {
            int index = MusicRecords.FindIndex(record1 => record1.Id == id);
            if (index >= 0)
            {
                record.Id = id;
                MusicRecords[index] = record;
            }
        }

        // DELETE: api/MusicRecords/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return MusicRecords.RemoveAll(record => record.Id == id);
        }
    }
}
