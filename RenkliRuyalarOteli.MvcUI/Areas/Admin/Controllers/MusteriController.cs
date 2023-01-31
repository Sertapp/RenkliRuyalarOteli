using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.Entities.Entities.Concrete;
using System.Security.Claims;

namespace RenkliRuyalarOteli.MvcUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MusteriController : Controller
    {
        private readonly IMusteriManager musteriManager;

        public MusteriController(IMusteriManager musteriManager)
        {
            this.musteriManager = musteriManager;
        }
        public async Task<IActionResult> Index()
        {
            var kullaniciId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Musteri musteri = new();
            musteri.KullaniciId = Guid.Parse(kullaniciId);
            var result = await musteriManager.FindAllAsync();
            return View(result);
        }
    }
}
