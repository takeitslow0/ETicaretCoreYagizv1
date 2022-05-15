#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Services.Bases;
using Business.Models;

namespace MvcWebUI.Controllers
{
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
        public IActionResult Delete(int? id)
        {
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
