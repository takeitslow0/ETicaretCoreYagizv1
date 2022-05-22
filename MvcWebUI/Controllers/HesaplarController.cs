using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MvcWebUI.Controllers
{
    public class HesaplarController : Controller
    {
        private readonly IHesapService _hesapService;

        private readonly IUlkeService _ulkeService;

        public HesaplarController(IHesapService hesapService)
        {
            _hesapService = hesapService;
        }

        // GET
        public IActionResult Giris()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Giris(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {
              var result =  _hesapService.Giris(model);
                if (result.IsSuccessful)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,result.Data.KullaniciAdi),
                        new Claim(ClaimTypes.Role, result.Data.RolAdiDisplay),  
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult YetkisizIslem()
        {
            return View("Hata", "Bu işlem için yetkiniz bulunmamaktadır !");
        }
        public IActionResult Kayit()
        {
            return View();
           var result = ViewBag.Ulkeler = _ulkeService.Query().ToList();
            if (result.IsSuccessful)
                ViewBag.UlkeId = new SelectList(result.Data, "Id", "Adi");
            else
                ViewBag.Mesaj = result.Message;
            return View();

        }
    }
}
