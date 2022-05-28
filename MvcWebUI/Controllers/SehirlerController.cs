using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    public class SehirlerController : Controller
    {
        private readonly ISehirService _sehirService;

        public SehirlerController(ISehirService sehirService)
        {
            _sehirService = sehirService;
        }

        public IActionResult SehirlerGet() //Sehirler/SehirlerGet/1
        {
            //var model = _sehirService.Query().ToList();
            var result = _sehirService.List();
            var model = result.Data;
            return Json(model);
            
        }
    }
}
