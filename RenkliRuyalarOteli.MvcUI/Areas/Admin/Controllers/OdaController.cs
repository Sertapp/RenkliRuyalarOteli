using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenkliRuyalarOteli.BL.Abstract;

namespace RenkliRuyalarOteli.MvcUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OdaController : Controller
    {
        private readonly IOdaManager odaManager;

        public OdaController(IOdaManager odaManager)
        {
            this.odaManager = odaManager;
        }
        public async Task<IActionResult> Index()
        {
            var result = await odaManager.FindAllAsync();
            return View(result);
        }
    }
}
