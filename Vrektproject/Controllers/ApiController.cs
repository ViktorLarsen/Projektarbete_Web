using System;
using System.Collections.Generic;
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
            //var userId = _userManager.GetUserId(User);
            //var user = _userManager.FindByIdAsync(User.Identity);
            //var user = GetCurrentUser();
            //var userId = user.Result.Id;
            List<string> Profiles = new List<string>();
            foreach (var profile in _context.Profiles)
            {
                Profiles.Add(/*JsonConvert.SerializeObject(profile)*/ id);
            }
            return Profiles;
        }

        //[HttpGet]
        //public async Task<ApplicationUser> GetCurrentUser()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }
        //    return user;
        //}

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