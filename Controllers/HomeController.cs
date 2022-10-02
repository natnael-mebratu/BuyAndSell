using Buy_And_Sell.Data;
using Buy_And_Sell.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Buy_And_Sell_Demo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult VehicleIndexH()
        {
            IEnumerable<Vehicle> objList = _db.Vehicle;

            return View(objList);
        }

        [AllowAnonymous]
        public IActionResult HouseIndexH()
        {
            IEnumerable<House> objList = _db.House;

            return View(objList);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Vehicle.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [AllowAnonymous]
        public IActionResult DetailH(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.House.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [AllowAnonymous]
        public IActionResult SellerInfo(string name)
        {
            var prodquery = from x in _db.Seller select x;

            if (!string.IsNullOrEmpty(name))
            {
                prodquery = prodquery.Where(x => x.FullName.Contains(name));
            }
            return View(prodquery.FirstOrDefault());

        }

        //Search
        [HttpGet]
        [AllowAnonymous]
        public IActionResult VehicleIndexH(string prodSearch)
        {
            ViewData["getproduct"] = prodSearch;
            var prodquery = from x in _db.Vehicle select x;

            if (!string.IsNullOrEmpty(prodSearch))
            {
                prodquery = prodquery.Where(x => x.Name.Contains(prodSearch) || x.Model.Contains(prodSearch)); ;
            }
            return View(prodquery.ToList());
        }
        //search House
        [HttpGet]
        [AllowAnonymous]
        public IActionResult HouseIndexH(string prodSearch)
        {
            ViewData["getproduct"] = prodSearch;
            var prodquery = from x in _db.House select x;

            if (!string.IsNullOrEmpty(prodSearch))
            {
                prodquery = prodquery.Where(x => x.Type.Contains(prodSearch) || x.Location.Contains(prodSearch));
            }
            return View(prodquery.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
