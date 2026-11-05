using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicRecords.Models;
using MusicRecords.Repositories;

namespace MusicRecords.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MusicRecordsController : ControllerBase
{
    private readonly IMusicRecordsRepository _musicRecordsRepository;

    public MusicRecordsController(IMusicRecordsRepository musicRecordsRepository)
    {
        _musicRecordsRepository = musicRecordsRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<MusicRecord>> Get([FromQuery] string? title = null, [FromQuery] string? artist = null)
        => Ok(_musicRecordsRepository.GetAll(title, artist));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MusicRecord), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MusicRecord> GetById(int id)
    {
        var record = _musicRecordsRepository.GetById(id);
        return record is null ? NotFound() : Ok(record);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(typeof(MusicRecord), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MusicRecord> Put(int id, [FromBody] MusicRecord record)
    {
        try
        {
            var updated = _musicRecordsRepository.Update(id, record);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }
}
