using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASR.Models;
using System.Globalization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace ASR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class SlotController : ControllerBase
    {
        private readonly ASRContext _context;

        public SlotController(ASRContext context)
        {
            _context = context;
        }

        // GET: api/Slot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> Get(string staffID, string studentID)
        {
            var slots = _context.Slot.Include(s => s.Staff)
                .Include(s => s.Student)
                .AsQueryable();

            if (staffID != null)
            {
                slots = slots.Where(s => s.Staff.SchoolID == staffID);
            }
            if (studentID != null)
            {
                slots = slots.Where(s => s.Student.SchoolID == studentID);
            }

            return await slots.ToListAsync();
        }


        // GET: api/Slot/<room-id>/<iso-start-time>
        [HttpGet("{roomID}/{startTime}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Slot>> GetSlot(string roomID, DateTime startTime)
        {
            // get the slot with a given datetime and roomid, and include the staff and students
            var slot = await _context.Slot.Include(s => s.Staff)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(s => s.StartTime == startTime && s.RoomID == roomID);

            if (slot == null)
            {
                return NotFound();
            }

            return slot;
        }


        // PUT: api/Slot/<room-id>/<iso-start-time>
        [HttpPut("{roomID}/{startTime}")]
        [ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<ActionResult<Slot>> PutSlotStudent(string roomID, DateTime startTime, [FromBody] string studentID)
        {
            var slot = await _context.Slot.FirstOrDefaultAsync(s => s.RoomID == roomID && s.StartTime == startTime);

            if (slot == null)
            {
                return NotFound();
            }

            if (String.IsNullOrEmpty(studentID))
            {
                slot.StudentID = null;
            }
            else
            {
                var student = await _context.Users.FirstOrDefaultAsync(u => u.SchoolID == studentID);

                if (student == null)
                {
                    return BadRequest();
                }
                slot.StudentID = student.Id;
            }
            
            _context.Entry(slot).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Slot/<room-id>/<iso-start-time>
        [HttpDelete("{roomID}/{startTime}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string roomID, DateTime startTime)
        {
            var slot = await _context.Slot.FirstOrDefaultAsync(s => s.StartTime == startTime && s.RoomID == roomID);

            if (slot == null)
            {
                return NotFound();
            }

            _context.Slot.Remove(slot);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
