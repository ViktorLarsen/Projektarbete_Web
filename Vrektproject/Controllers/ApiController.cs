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

namespace Vrektproject.Controllers
{
    //[Authorize]
    //[Route("[controller]/[action]")]
    [Produces("application/json")]
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
        public string MysteryAnimal()
        {
            Random rnd = new Random();
            return lista[rnd.Next(0, lista.Count)];
        }


        [HttpGet]
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
                var profileId = _context.Users.Where(u => u.Id == userId).Single().ProfileId;
                profiles.Add(_context.Profiles.Where(p => p.Id == profileId).Single());
            }

            //TODO: Compare and count number of matching skills between user profile and other relevant profiles, then sort list of profiles (dictionary?)

            List<string> ProfilesJson = new List<string>();
            foreach (var profile in profiles)
            {
                ProfilesJson.Add(JsonConvert.SerializeObject(profile));
            }
            return ProfilesJson;
        }

        [HttpGet]
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