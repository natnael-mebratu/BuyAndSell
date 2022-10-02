using Buy_And_Sell.Data;
using Buy_And_Sell.Models;
using Buy_And_Sell.ViewModel;
using Buy_And_Sell_Demo.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.HttpContext.Current.Session["SessionString"] = "sample";

namespace Buy_And_Sell.Controllers
{
    public class SellerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SellerController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        //Get Account
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Seller obj)
        {
            if (ModelState.IsValid)
            {
                _db.Seller.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("VehicleIndex");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult IndexH(Seller obj)
        {
            if (ModelState.IsValid)
            {
                _db.Seller.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("HouseIndex");
            }
            return View(obj);
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
            .Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            //        var Email = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

            return RedirectToAction("Index");
            //    return Json(claims);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        //Vehicle CRUD

        public IActionResult VehicleIndex()
        {
            IEnumerable<Vehicle> objList = from x in _db.Vehicle select x;
            objList = objList.Where(x => x.SellerName == User.Identity.Name);
            return View(objList);
        }
        public IActionResult VehicleAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VehicleAdd(VehicleViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string stringFileName = UploadFile(obj);
                var vehicle = new Vehicle
                {
                    Name = obj.Name,
                    Model = obj.Model,
                    Year = obj.Year,
                    Price = obj.Price,
                    Discription = obj.Discription,
                    Image = stringFileName,
                    SellerName = obj.SellerName
                };
                //Insert record
                _db.Vehicle.Add(vehicle);
                _db.SaveChanges();
                return RedirectToAction("VehicleIndex");
            }
            return View(obj);
        }

        private string UploadFile(VehicleViewModel obj)
        {
            //save image to wwwroot/Image
            string fileName = null;
            if (obj.Image != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + obj.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult VehicleEdit(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VehicleEdit(Vehicle obj)
        {
            if (ModelState.IsValid)
            {
                _db.Vehicle.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("VehicleIndex");
            }
            return View(obj);
        }
        public IActionResult VehicleDelete(int? id)
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
        [HttpPost]
        //   [ValidateAntiForgeryToken]
        public IActionResult VehicleDeletePost(int? id)
        {
            var obj = _db.Vehicle.Find(id);

            //Delete from wwwrootfolder /Images
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", obj.Name);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            //Delete from database
            if (obj == null)
            {
                return NotFound();
            }
            _db.Vehicle.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("VehicleIndex");
        }


        //House CRUD


        public IActionResult HouseIndex()
        {
            IEnumerable<House> objList = from x in _db.House select x;
            objList = objList.Where(x => x.SellerName == User.Identity.Name);
            return View(objList);
        }
        public IActionResult HouseAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HouseAdd(HouseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string stringFileName = UploadFileH(obj);
                var house = new House
                {
                    Type = obj.Type,
                    Size = obj.Size,
                    NumberOfBedRooms = obj.NumberOfBedRooms,
                    Location = obj.Location,
                    Level = obj.Level,
                    Price = obj.Price,
                    Discription = obj.Discription,
                    Image = stringFileName,
                    SellerName = obj.SellerName


                };
                //Insert record
                _db.House.Add(house);
                _db.SaveChanges();
                return RedirectToAction("HouseIndex");
            }
            return View(obj);
        }

        private string UploadFileH(HouseViewModel obj)
        {
            //save image to wwwroot/Image
            string fileName = null;
            if (obj.Image != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + obj.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public IActionResult HouseEdit(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HouseEdit(House obj)
        {
            if (ModelState.IsValid)
            {
                _db.House.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("HouseIndex");
            }
            return View(obj);
        }
        public IActionResult HouseDelete(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HouseDeletePost(int? id)
        {
            var obj = _db.House.Find(id);

            //Delete from wwwrootfolder /Images
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", obj.Type);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            //Delete from database
            if (obj == null)
            {
                return NotFound();
            }

            _db.House.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("HouseIndex");
        }




    }
}
