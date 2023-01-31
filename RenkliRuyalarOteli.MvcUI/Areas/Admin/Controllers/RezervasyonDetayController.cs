using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenkliRuyalarOteli.BL.Abstract;

namespace RenkliRuyalarOteli.MvcUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RezervasyonDetayController : Controller
    {
        private readonly IRezervasyonDetayManager rezervasyonDetayManager;
        public RezervasyonDetayController(IRezervasyonDetayManager rezervasyonDetayManager)
        {
            this.rezervasyonDetayManager= rezervasyonDetayManager;
        }
        public async Task<IActionResult> Index()
        {
            var result=await rezervasyonDetayManager.FindAllAsync();
            return View(result);
        }
    }
}
