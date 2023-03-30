using Crud.Api.Entities;
using Crud.Api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Include(de => de.Speakers).Where(d => !d.IsDeleted).ToList();
            if (devEvents == null)
            {
                return NotFound();
            }
            return Ok(devEvents);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEventById = _context.DevEvents.Include(de => de.Speakers).Where(d => d.Id == id).ToList();
            if (devEventById == null)
            {
                return NotFound();
            }
            return Ok(devEventById);
        }
        [HttpPost]
        public IActionResult Post(DevEvents devEvent)
        {
            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = devEvent.Id}, devEvent);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DevEvents devEvent)
        {
            var devEventToChange = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEventToChange == null)
            {
                return NotFound();
            }
            devEventToChange.Update(devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate);
            _context.DevEvents.Update(devEventToChange);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var devEventToDelete = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEventToDelete == null)
            {
                return NotFound();
            }
            devEventToDelete.Delete();
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Speakers.Add(speaker);
            _context.SaveChanges();

            return Ok(devEvent);
        }
    }
}
