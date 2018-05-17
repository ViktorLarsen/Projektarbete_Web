using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
//using Google.Apis.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vrektproject.Data;
using Vrektproject.Models;
using Vrektproject.Services;

namespace Vrektproject.Controllers
{
    [Authorize]
  //  [Route("[controller]/[action]")]
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Från här
        private readonly UserManager<ApplicationUser> _userManager;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);
        // Till här

        public SkillsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            // usermanager added
            _userManager = userManager;
            //
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            //user added
            var user = await _userManager.GetUserAsync(User);
            
            var applicationDbContext = _context.Skills.Where(s => s.Profile.Id == user.ProfileId);

            return View(await applicationDbContext.ToListAsync());
        }

 
        // GET: Skills/Create
        public IActionResult Create()
        {

            var user = _userManager.GetUserAsync(User);             
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", user.Id);
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProfileId")] Skill skill)
        {
            // user added
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {                           
                _context.Add(skill);
                skill.ProfileId = user.ProfileId.GetValueOrDefault();
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", skill.Id);
            return View(skill);
        }

  

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .Include(s => s.Profile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(m => m.Id == id);
            _context.Skills.Remove(skill);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
