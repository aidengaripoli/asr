using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASR.Models;

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
        public IEnumerable<Room> Get()
        {
            return _context.Room.ToList();
        }

        // GET: api/Room/<room-id>
        [HttpGet("{id}", Name = "GetRoom")]
        public Room Get(string id)
        {
            var room = _context.Room.FirstOrDefault( r => r.RoomID == id);
            return room;
        }

        // POST: api/Room
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
