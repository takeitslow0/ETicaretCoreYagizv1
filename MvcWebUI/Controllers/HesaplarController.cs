using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    public class HesaplarController : Controller
    {
        private readonly IHesapService _hesapService;

        public HesaplarController(IHesapService hesapService)
        {
            _hesapService = hesapService;
        }

        // GET
        public IActionResult Giris()
        {
            return View();
        }
    }
}
