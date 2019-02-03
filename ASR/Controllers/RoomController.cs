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
            var slotsOnDate =  _context.Slot.Where(s => s.StartTime == viewModel.StartTime);
            
            var availableRooms = from r in _context.Room
                        join s in slotsOnDate on r.RoomID equals s.RoomID into x
                        from p in x.DefaultIfEmpty()
                        group p by r.RoomID into g
                        where g.Count() < 2 || g == null
                        select new Room()
                        {
                            RoomID = g.Key
                        };

            return View(await availableRooms.ToListAsync());
        }
    }
}
