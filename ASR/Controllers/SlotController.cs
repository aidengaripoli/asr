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

namespace ASR.Controllers
{
    public class SlotController : Controller
    {
        // GET: Slot

        private readonly ASRContext _context;

        public SlotController(ASRContext context)
        {
            _context = context;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            var slots = await _context.Slot.Include(s => s.Staff).Include(s => s.Student).ToListAsync();
            return View(slots);
        }

        // GET: Slot/Create
        [Authorize(Roles = Constants.StaffRole)]
        public IActionResult Create()
        {
            return View();
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

            if (slot.StudentID == null)
            {
                ApplicationUser currentUser = _context.ApplicationUser
                                                .FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
                slot.StudentID = currentUser.Id;

            }
            else
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
            _context.Remove(slot);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}