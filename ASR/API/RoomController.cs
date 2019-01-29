using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASR.Models;
using Microsoft.EntityFrameworkCore;

namespace ASR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ASRContext _context;

        public RoomController(ASRContext context)
        {
            _context = context;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>>  Get()
        {
            return await _context.Room.ToListAsync();
        }

        // GET: api/Room/<room-id>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Room>> GetRoom(string id)
        {
            var room = await _context.Room.FirstOrDefaultAsync( r => r.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }

        // POST: api/Room
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Room>> Post(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomID }, room);
        }

        // PUT: api/Room/<room-id>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(string id, Room room)
        {
            if (id != room.RoomID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Slot/<room-id>
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string id)
        {
            var room = await _context.Room.FirstOrDefaultAsync(r => r.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
