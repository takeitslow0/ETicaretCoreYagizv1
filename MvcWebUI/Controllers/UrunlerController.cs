#nullable disable
using Business.Models;
using Business.Services;
using Business.Services.Bases;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcWebUI.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        private readonly ETicaretContext _context;
        private readonly IKategoriService _kategoriService;

        //public UrunlerController(ETicaretContext context)
        //{
        //    _context = context;
        //}
        private readonly IUrunService _urunService;

        public UrunlerController(IUrunService urunService, IKategoriService kategoriService)
        {
            _urunService = urunService;
            _kategoriService = kategoriService;
        }

        // GET: Urunler
        //public IActionResult Index()
        //{
        //    var eTicaretContext = _context.Urunler.Include(u => u.Kategori);
        //    return View(eTicaretContext.ToList());
        //}
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _urunService.Query().ToList();
            return View(model);
        }

        // GET: Urunler/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return View("Hata");
            }

            UrunModel urun = _urunService.Query().SingleOrDefault(u => u.Id == id.Value);
            if(urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }

            return View(urun);
        }

        // GET: Urunler/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi");
            //return View();
            UrunModel model = new UrunModel()
            {
                SonKullanmaTarihi = DateTime.Today,
                BirimFiyati = 0,
                StokMiktari = 0
            };
            return View(model);
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public IActionResult Create(UrunModel urun)
        {
            if (ModelState.IsValid)
            {
                var result = _urunService.Add(urun);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.TryAddModelError("", result.Message);
            }
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // GET: Urunler/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id) // Urunler/Edit
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            UrunModel urun = _urunService.Query().SingleOrDefault(u => u.Id == id);
            if (urun == null)
                return View("Hata", "Ürün bulunamadı!");
            ViewBag.KategoriId = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // POST: Urunler/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(UrunModel urun)
        {
            if (ModelState.IsValid)
            {
                var result = _urunService.Update(urun);
                if (result.IsSuccessful)
                    //return Redirect("https://bilgeadam.com");
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message); //ELSE YAZMAMIZA GEREK YOK
            }
            ViewBag.KategoriId = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // GET: Urunler/Delete/5
        //[Authorize(Roles = "Admin")]

        public IActionResult Delete(int? id)
        {
            if (!(User.Identity.IsAuthenticated && !User.IsInRole("Admin")))
                return RedirectToAction("YetkisizIslem", "Hesaplar"); // AUTHORIZE İLE AYNI ETKİ !            
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }

            var result = _urunService.Delete(id.Value);
            if (result.IsSuccessful)
                TempData["Succes"] = result.Message;
            else
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
        }
	}
}
