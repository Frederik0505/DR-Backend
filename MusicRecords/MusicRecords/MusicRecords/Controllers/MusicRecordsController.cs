using Microsoft.AspNetCore.Mvc;
using MusicRecords.Models;
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
        public ActionResult<IEnumerable<MusicRecord>> Get(
            [FromQuery] string? title = null,
            [FromQuery] string? artist = null)
        {
            return Ok(_musicRecordsRepository.GetAll(title, artist));
        }

        // US5: DELETE api/MusicRecords/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            // Forsøger at slette pladen med det angivne ID
            bool deleted = _musicRecordsRepository.Delete(id);

            // Hvis pladen ikke findes, returneres 404
            if (!deleted)
            {
                return NotFound();
            }

            // Hvis pladen blev slettet, returneres 200
            return Ok();
        }
    }
}
