using MusicRecords.Models;
using Microsoft.AspNetCore.Mvc;
using MusicRecords.Repositories;

namespace MusicRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicRecordsController : ControllerBase
    {
        private readonly IMusicRecordsRepository _musicRecordsRepository;

        public MusicRecordsController(IMusicRecordsRepository musicRecordsRepository)
        {
            _musicRecordsRepository = musicRecordsRepository;
        }

        // GET: api/MusicRecords
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<MusicRecord> Get()
        {
            return Ok(_musicRecordsRepository.GetAll());
        }

        // POST: api/MusicRecords
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MusicRecord> Post(MusicRecord record)
        {
            if (record == null || !record.IsValid())
            {
                return BadRequest();
            }

            var newRecord = _musicRecordsRepository.Add(record);

            if (newRecord == null)
            {
                return BadRequest();
            }

            return StatusCode(201, newRecord);
        }
    }
}
