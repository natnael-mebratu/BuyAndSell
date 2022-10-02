using Microsoft.AspNetCore.Mvc;

namespace Buy_And_Sell_Demo.Controllers
{
    public class BuyerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //after selecting a product, enter identification of buyer to database
    }
}
