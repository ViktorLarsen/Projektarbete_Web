using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vrektproject.Models.ManageViewModels;

namespace Vrektproject.Controllers
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private List<string> lista = new List<string> { "Dog", "Cat", "Giraffe", "Pig", "Lion", "Camel", "Bear", "Badger", "Coyote", "Deer", "Elk", "Dolphin" };

        [HttpGet]
        public string MysteryAnimal()
        {
            Random rnd = new Random();
            return lista[rnd.Next(0, lista.Count)];
        }
    }
}