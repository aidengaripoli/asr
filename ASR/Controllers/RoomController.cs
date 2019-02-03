using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASR.Models;
using Microsoft.AspNetCore.Authorization;
using ASR.Data;

namespace ASR.Controllers
{
    public class RoomController : Controller
    {
        private readonly ASRContext _context;

        public RoomController(ASRContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(new RoomViewModel
            {
                Rooms = await _context.Room.ToListAsync(),
                StartTime = DateTime.Now.Date + new TimeSpan(9, 0, 0)
            });
        }

        // GET: Room/Availability
        [Authorize(Roles = Constants.StaffRole)]
        public async Task<IActionResult> Availability(RoomViewModel viewModel)
        {
            // gets all rooms for a given date
            var roomIDsForDate = _context.Slot.Where(x => x.StartTime.Date == viewModel.StartTime)
                .Select(x => x.RoomID)
                .ToList();

            // get the rooms that have been booked twice or more
            var excludeRooms = roomIDsForDate.GroupBy(x => x)
                .Where(g => g.Count() >= 2)
                .Select(x => x.Key);

            // get the rooms that are available
            var availableRooms = _context.Room.Where(x => !excludeRooms.Contains(x.RoomID));

            return View(await availableRooms.ToListAsync());
        }
    }
}
