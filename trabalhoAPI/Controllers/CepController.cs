using Microsoft.AspNetCore.Mvc;
using trabalhoAPI.Service;

namespace trabalhoAPI.Controllers
{
    public class CepController : Controller
    {
        private readonly CorreiosService _correiosService;

        public CepController(CorreiosService correiosService)
        {
            _correiosService = correiosService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
