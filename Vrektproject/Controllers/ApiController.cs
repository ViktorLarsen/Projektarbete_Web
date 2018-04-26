using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vrektproject.Data;
using Vrektproject.Models;

namespace Vrektproject.Controllers
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        private List<string> lista = new List<string> { "Dog", "Cat", "Giraffe", "Pig", "Lion", "Camel", "Bear", "Badger", "Coyote", "Deer", "Elk", "Dolphin" };

        [HttpGet]
        public string MysteryAnimal()
        {
            Random rnd = new Random();
            return lista[rnd.Next(0, lista.Count)];
        }


        [HttpGet]
        public List<string> GetProfiles()
        {
            List<string> Profiles = new List<string>();
            foreach (var profile in _context.Profiles)
            {
                Profiles.Add(JsonConvert.SerializeObject(profile));
            }
            return Profiles;
        }

        public IActionResult Finder()
        {
            return View();
        }
    }
}