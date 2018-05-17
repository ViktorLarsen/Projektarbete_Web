using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vrektproject.Data;
using Vrektproject.Models;
using Vrektproject.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;

namespace Vrektproject.Controllers
{
    [Produces("application/json")]
    [Route("Api/Finder")]
    public class ApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private List<string> lista = new List<string> { "Dog", "Cat", "Giraffe", "Pig", "Lion", "Camel", "Bear", "Badger", "Coyote", "Deer", "Elk", "Dolphin" };

        [HttpGet]
        [Route("MysteryAnimal")]
        public string MysteryAnimal()
        {
            Random rnd = new Random();
            return lista[rnd.Next(0, lista.Count)];
        }


        [HttpGet]
        [Route("GetProfiles")]
        public List<string> GetProfiles(string id)
        {
            ApplicationUser user;
            List<Profile> profiles = new List<Profile>();
            List<string> userIds = new List<string>();

            try
            {
                user = _context.Users.Where(u => u.Id == id).Single(); //Get active user
            }
            catch
            {
                throw new ApplicationException($"Unable to load user with ID '{id}'.");
            }

            var roleId = _context.UserRoles.Where(r => r.UserId == user.Id).Single().RoleId; //Get active user role

            //Get list of opposite role user IDs
            if (roleId == "1")
            {
                foreach (var entity in _context.UserRoles)
                {
                    if (entity.RoleId == "2")
                    {
                        userIds.Add(entity.UserId);
                    }
                }
            }
            else
            {
                foreach (var entity in _context.UserRoles)
                {
                    if (entity.RoleId == "1")
                    {
                        userIds.Add(entity.UserId);
                    }
                }
            }

            //For each found user, add their profiles to list
            foreach (var userId in userIds)
            {
                if (roleId == "1" && _context.Likes.Where(l => l.Member.Id == id && l.Recruiter.Id == userId).Any())
                {
                    if (!_context.Likes.Where(l => l.Member.Id == id && l.Recruiter.Id == userId).Single().MemberLike)
                    {
                        var profileId = _context.Users.Where(u => u.Id == userId).Single().ProfileId;
                        profiles.Add(_context.Profiles.Where(p => p.Id == profileId).Single());
                    }
                }
                else if (roleId == "1") 
                {
                    var profileId = _context.Users.Where(u => u.Id == userId).Single().ProfileId;
                    profiles.Add(_context.Profiles.Where(p => p.Id == profileId).Single());
                }
                if (roleId == "2" && _context.Likes.Where(l => l.Member.Id == userId && l.Recruiter.Id == id).Any())
                {
                    if (!_context.Likes.Where(l => l.Member.Id == userId && l.Recruiter.Id == id).Single().RecruiterLike)
                    {
                        var profileId = _context.Users.Where(u => u.Id == userId).Single().ProfileId;
                        profiles.Add(_context.Profiles.Where(p => p.Id == profileId).Single());
                    }
                }
                else if(roleId == "2")
                {
                    var profileId = _context.Users.Where(u => u.Id == userId).Single().ProfileId;
                    profiles.Add(_context.Profiles.Where(p => p.Id == profileId).Single());
                }
            }

            //TODO: Compare and count number of matching skills between user profile and other relevant profiles, then sort list of profiles (dictionary?)

            List<string> ProfilesJson = new List<string>();
            foreach (var profile in profiles)
            {
                var formattedProfile = new FormattedProfile
                {
                    UserId = _context.Users.Where(u => u.ProfileId == profile.Id).Single().Id,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Description = profile.Description,
                    Company = profile.Company,
                    AvatarImage = profile.AvatarImage
                };
                ProfilesJson.Add(JsonConvert.SerializeObject(formattedProfile));
            }
            return ProfilesJson;
        }

        public class FormattedProfile
        {
            public string UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Description { get; set; }
            public string Company { get; set; }
            public byte[] AvatarImage { get; set; }
        }

        [HttpGet]
        [Route("Like")]
        public List<string> Like(string id, string likeId)
        {
            var isRecruiter = _context.Users.Where(u => u.Id == id).Single().RoleIdentifier == 2;
            if (isRecruiter)
            {
                if (!_context.Likes.Where(l => l.Member.Id == likeId && l.Recruiter.Id == id).Any())
                {
                    var like = new Like
                    {
                        Member = _context.Users.Where(u => u.Id == likeId).Single(),
                        MemberLike = false,
                        Recruiter = _context.Users.Where(u => u.Id == id).Single(),
                        RecruiterLike = true,
                    };
                    _context.Add(like);
                }
                else
                {
                    _context.Likes.Where(l => l.Member.Id == likeId && l.Recruiter.Id == id).Single().RecruiterLike = true;
                }
            }
            else
            {
                if (!_context.Likes.Where(l => l.Member.Id == id && l.Recruiter.Id == likeId).Any())
                {
                    var like = new Like
                    {
                        Member = _context.Users.Where(u => u.Id == id).Single(),
                        MemberLike = true,
                        Recruiter = _context.Users.Where(u => u.Id == likeId).Single(),
                        RecruiterLike = false,
                    };
                    _context.Add(like);
                }
                else
                {
                    _context.Likes.Where(l => l.Member.Id == id && l.Recruiter.Id == likeId).Single().MemberLike = true;
                }
            }
            _context.SaveChanges();
            return GetProfiles(id);
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ApplicationUser { Id = user.Id };

            return View(model);
        }
    }
}