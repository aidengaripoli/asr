﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ASR.Data;

namespace ASR.Controllers
{
    [Authorize(Roles = Constants.StaffRole)]
    public class StaffController : Controller
    {
        private readonly ASRContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StaffController(ASRContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            var staff = await _userManager.GetUsersInRoleAsync(Constants.StaffRole);
            return View(staff);
        }
    }
}
