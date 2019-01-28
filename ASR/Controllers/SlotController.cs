using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ASR.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASR.Controllers
{
    public class SlotController : Controller
    {
        private readonly ASRContext _context;

        public SlotController(ASRContext context)
        {
            _context = context;
        }

        // GET: Slot
        public async Task<IActionResult> Index()
        {
            var staff = _context.ApplicationUser.Where(x => x.SchoolID.StartsWith('e'));

            var slots = _context.Slot.Select(x => x).Include(x => x.Student).Include(x => x.Staff);

            var startDate = DateTime.Now;
            var startTime = new TimeSpan(9, 0, 0);
            startDate = startDate.Date + startTime;

            return View(new SlotStaffViewModel
            {
                Slots = await slots.ToListAsync(),
                Staff = new SelectList(await staff.ToListAsync(), "SchoolID", "SchoolID"),
                StartTime = startDate
            });
        }

        // GET: Slot/Create
        [Authorize(Roles = Constants.StaffRole)]
        public async Task<IActionResult> Create()
        {
            var rooms = _context.Room.Select(x => x.RoomID);

            return View(new SlotRoomsViewModel
            {
                Rooms = new SelectList(await rooms.ToListAsync())
            });
        }

        // POST: Slot/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.StaffRole)]
        public async Task<IActionResult> Create(Slot slot)
        {

            ApplicationUser currentUser = _context.ApplicationUser
                .FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            slot.StaffID = currentUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(slot);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(slot);
        }

        // POST: Slot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.StudentRole)]
        public async Task<IActionResult> Book(string roomID, DateTime startTime)
        {
            var slot = _context.Slot.FirstOrDefault(s => s.RoomID == roomID && s.StartTime == startTime);

            ApplicationUser currentUser = _context.ApplicationUser
                    .FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            if (slot.StudentID == null)
            {
                slot.StudentID = currentUser.Id;
            }
            else if (slot.StudentID == currentUser.Id)
            {
                slot.StudentID = null;
            }

            _context.Update(slot);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Slot/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.StaffRole)]
        public async Task<IActionResult> Delete(string roomID, DateTime startTime)
        {
            var slot = _context.Slot.FirstOrDefault(s => s.RoomID == roomID && s.StartTime == startTime);

            if (slot.StudentID != null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Remove(slot);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Slot/Availability
        [Authorize(Roles = Constants.StudentRole)]
        public async Task<IActionResult> Availability(Slot slot, string staffID)
        {
            var availableSlots = _context.Slot.Where(x => x.Staff.SchoolID == staffID)
                .Where(x => x.StartTime == slot.StartTime)
                .Where(x => x.StudentID == null)
                .Include(s => s.Staff);

            return View(await availableSlots.ToListAsync());
        }
    }
}
