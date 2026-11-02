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
        private static int _nextId = 4;

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

       
    }
}
