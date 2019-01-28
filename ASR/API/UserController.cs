using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASR.Data;
using ASR.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net;

namespace ASR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ASRContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ASRContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/User/<role>
        [HttpGet("{role}", Name = "GetUsers")]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get(string role)
        {
            if (role.ToLower() == Constants.StaffRole.ToLower())
            {
                var staffs = await _userManager.GetUsersInRoleAsync(Constants.StaffRole);
                return staffs.ToList();
            }
            else if (role.ToLower() == Constants.StudentRole.ToLower())
            {
                var students = await _userManager.GetUsersInRoleAsync(Constants.StudentRole);
                return students.ToList();
            }
            return BadRequest();

        }
    }
}
