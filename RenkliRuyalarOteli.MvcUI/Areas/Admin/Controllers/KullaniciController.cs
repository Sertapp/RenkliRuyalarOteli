using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.Entities.Entities.Concrete;
using RenkliRuyalarOteli.MvcUI.Areas.Admin.Models.Kullanici;

namespace RenkliRuyalarOteli.MvcUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KullaniciController : Controller
    {

        private readonly IKullaniciManager kullaniciManager;
        private readonly IRoleManager roleManager;

        public KullaniciController(IKullaniciManager kullaniciManager, IRoleManager roleManager)
        {
            this.roleManager = roleManager;
            this.kullaniciManager = kullaniciManager;
        }
        public async Task<IActionResult> Index()
        {

            var result = await kullaniciManager.FindAllIncludeAsync(null, p => p.Roller);
            return View(result.ToList());
        }
        public async Task<IActionResult> Kayit()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var kullanici = kullaniciManager.FindAsync(p => p.Id == Id).Result;

            KullaniciUpdateDTO updateDto = new KullaniciUpdateDTO
            {
                Id = kullanici.Id,
                Adi = kullanici.Ad,
                Soyadi = kullanici.Soyad,
                Email = kullanici.Email,
                Password = kullanici.Password,
                RePassword = kullanici.Password,
                DogumTarihi = (DateTime)kullanici.DogumTarihi,
                TcNo = kullanici.TcNo,
                Cinsiyet = (bool)kullanici.Cinsiyet,
                KullaniciAdi = kullanici.KullaniciAdi
            };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KullaniciUpdateDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDTO);
            }

            //Eger IFromFile Tipindeki prop bos degilse serverda upload klasorune kopyala
            // Ve Database'de ImageData alanina yazdir.
            var kul = kullaniciManager.FindAsync(p => p.Id == updateDTO.Id).Result;
            if (updateDTO.ImageFile != null)
            {
                var extent = Path.GetExtension(updateDTO.ImageFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", randomName);
                kul.ImageUrl = "Uploads\\" + randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await updateDTO.ImageFile.CopyToAsync(stream);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    //postedFile.CopyTo(ms);	
                    updateDTO.ImageFile.CopyTo(ms);
                    kul.ImageData = ms.ToArray();
                }
            }

            //UpdateDto Kullanici Tipine cevirmek gerekiyor


            kul.Ad = updateDTO.Adi;
            kul.Soyad = updateDTO.Soyadi;
            kul.DogumTarihi = updateDTO.DogumTarihi;
            kul.Password = updateDTO.Password;
            kul.UpdateDate = DateTime.Now;

            var sonuc = await kullaniciManager.UpdateAsync(kul);
            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Kullanici");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Lutfen Biraz sonra tekrar denbeyiniz");

                return View(updateDTO);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var createDto = new KullaniciCreateDTO();

            @ViewBag.Roller = GetRoller();
            return View(createDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KullaniciCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                @ViewBag.Roller = GetRoller();
                return View(createDTO);
            }

            var kullanici = new Kullanici
            {
                Ad = createDTO.Adi,
                Soyad = createDTO.Soyadi,
                Cinsiyet = true,
                DogumTarihi = createDTO.DogumTarihi,
                Email = createDTO.EmailAddress,
                TcNo = createDTO.TcNo,

            };

            #region Eger role atanmis ise

            //if (createDTO.Roller.Count > 0)
            //{
            //    foreach (var roleId in createDTO.Roller)
            //    {
            //        var role = await roleManager.GetByIdAsync(roleId);

            //        kullanici.Roller.Add(role);

            //    }
            //} 
            #endregion
            #region Eger Image Seciliyse

            if (createDTO.ImageFile != null)
            {
                var extent = Path.GetExtension(createDTO.ImageFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", randomName);
                kullanici.ImageUrl = "Uploads\\" + randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await createDTO.ImageFile.CopyToAsync(stream);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    //postedFile.CopyTo(ms);	
                    createDTO.ImageFile.CopyTo(ms);
                    kullanici.ImageData = ms.ToArray();
                }
            }
            #endregion


            var sonuc = await kullaniciManager.CreateAsync(kullanici);

            foreach (var item in createDTO.Roller)
            {


            }


            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Kullanici");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");
                @ViewBag.Roller = GetRoller();
                return View(createDTO);
            }

        }

        [NonAction]
        private List<SelectListItem> GetRoller()
        {
            var roller = roleManager.FindAllAsync(null).Result;

            List<SelectListItem> ListItemRols = new();

            //foreach (Role role in roller)
            //{
            //    ListItemRols.Add(new SelectListItem(role.RoleName, role.Id.ToString()));
            //}
            return ListItemRols;
        }
    }
}


