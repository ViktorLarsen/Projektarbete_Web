using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vrektproject.Data;
using Vrektproject.Models;

namespace Vrektproject.Controllers
{
    [Authorize]
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Från här
        private readonly UserManager<ApplicationUser> _userManager;
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);
        // Till här


        public LikesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            // usermanager added
            _userManager = userManager;
            //
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var applicationDbContext = _context.Likes.Where(s => s.Member.Id == user.Id && s.MemberLike == true && s.RecruiterLike == true || s.Recruiter.Id == user.Id && s.MemberLike == true && s.RecruiterLike == true);
            if (user.RoleIdentifier == 1)
            {

                applicationDbContext = applicationDbContext.Include(s => s.Recruiter.Profile);
            }
            else if (user.RoleIdentifier == 2)
            {
                applicationDbContext = applicationDbContext.Include(s => s.Member.Profile);
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }


        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var like = await _context.Likes
                  .SingleOrDefaultAsync(m => m.Id == id);

            if (user.RoleIdentifier == 1)
            {
                like = await _context.Likes.Include(s => s.Recruiter.Profile)
                   .SingleOrDefaultAsync(m => m.Id == id);
            }

            if (user.RoleIdentifier == 2)
            {
                like = await _context.Likes.Include(s => s.Member.Profile)
                   .SingleOrDefaultAsync(m => m.Id == id);
            }
            

            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var like = await _context.Likes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
