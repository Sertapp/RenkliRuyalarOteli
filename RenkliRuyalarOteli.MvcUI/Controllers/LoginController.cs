using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.MvcUI.Models;
using System.Security.Claims;

namespace RenkliRuyalarOteli.MvcUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IKullaniciManager kullaniciManager;

        public LoginController(IKullaniciManager kullaniciManager)
        {
            this.kullaniciManager = kullaniciManager;
        }
        public IActionResult Giris()
        {
            LoginVM login=new LoginVM();
           return View();   
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Giris(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var kullanici = kullaniciManager.FindAsync(p => p.Email == login.Email && p.Password == login.Password).Result;
            if (kullanici==null)
            {
                ModelState.AddModelError("", "Kullanici Adi Yada Sifre Hatalidir");
                return View(login);
            }

            //Claim 'leri olusturup cookie icerisine atalim. HEr bir Claim Kimlik karti 
            //uzerinde bulunacak alan olarak dusunulebilir.
            //new Claim(ClaimTypes.Name, kullanici.Email.Split("@")[0])
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,kullanici.Email),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Name,kullanici.Ad + " " + kullanici.Soyad),
                new Claim("TcNo",kullanici.TcNo),
                new Claim(ClaimTypes.NameIdentifier,kullanici.Id.ToString())


            };
            //Kimlik Kartini Olusturdugumuz yer. Kart uzerinde hangi lalanlarin oldugu bilgisi
            //claims Listesinde mevcuttur
           
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperty = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            // Kullanici icin  claimIdentity kimlik karti ile giris yapilmistir. 
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                                            , new ClaimsPrincipal(claimIdentity)
                                            , authenticationProperty);


            // Giris yapilmis ve kimlik karti olusturulmus kullanci Area icerisindeki Admin Bolumune yonlemndirildi
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}
