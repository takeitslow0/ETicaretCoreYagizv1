using AppCore.Business.Models.Result;
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    
    public class KategorilerController : Controller
    {
        private readonly IKategoriService _kategoriService;

        public KategorilerController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }

        [Authorize]
        public IActionResult Index() // ~/Kategoriler/
        {
            List<KategoriModel> model = _kategoriService.Query().ToList();
            //return ViewResult();
            return View(model); // Views/Kategoriler/Index.cshtml
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult OlusturGetir() // ~/Kategoriler/OlusturGetir
        {
            return View("OlusturHtml");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult OlusturGonder(string Adi, string Aciklamasi)
        {
            KategoriModel model = new KategoriModel()
            {
                Adi = Adi,
                Aciklamasi = Aciklamasi
            };
            var result = _kategoriService.Add(model);
            if (result.IsSuccessful)
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index)); // "Index"
            return View("Hata", result.Message);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id) //Kategoriler/Edit/1
        {
            if(id == null)
                return View("Hata", "Id gereklidir!");
            KategoriModel model = _kategoriService.Query().SingleOrDefault(k => k.Id == id);
            if (model == null)
                return View("Hata", "Kategori bulunamadı!");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(KategoriModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _kategoriService.Update(model);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int? id) // ~/Kategoriler/Dtails
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            KategoriModel model = _kategoriService.Query().SingleOrDefault(k => k.Id == id.Value); // value olmasa da olur.
            if (model == null)
                return View("Hata", "Kategori bulunamadı");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("Hata", "Id gereklidir!");
            Result result = _kategoriService.Delete(id.Value);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        /*
        IActionResult
        ActionResult
        ViewResult(View()) ContentResult (Content()) EmptyResult FileContentResult(File()) HttpResults JavaScriptResult(Javascript())
        JsonResult (Json()) RedirectResults
        */


        public ContentResult GetHtmlContent()
        {
            return Content("<b><i>Content result.</i></b>", "text/html");
        }

        public ActionResult GetKategorilerXmlContent()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            xml += "<KategoriModels>";
            xml += "<KategoriModel>";
            xml += "<Id>" + 1 + "</Id>";
            xml += "<Adi>" + "Laptop" + "</Adi>";
            xml += "<Aciklamasi>" + "Dizüstü Bilgisayarlar" + "</Aciklamasi>";
            xml += "</KategoriModel>";
            xml += "</KategoriModels>";
            return Content(xml, "application/xml");
        }

        public string GetString()
        {
            return "String.";
        }

        public EmptyResult GetEmpty()
        {
            return new EmptyResult();
        }
    }
}
