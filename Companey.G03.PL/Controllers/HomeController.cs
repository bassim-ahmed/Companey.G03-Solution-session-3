using Companey.G03.PL.ViewModels;
using Company.G03.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Companey.G03.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scoped01;
        private readonly IScopedService scoped02;
        private readonly ITranslentService translent01;
        private readonly ITranslentService translent02;
        private readonly ISingeltonService singelton01;
        private readonly ISingeltonService singelton02;

        public HomeController(
            
            ILogger<HomeController> logger,
            IScopedService scoped01,
                IScopedService scoped02,
              ITranslentService translent01,
                ITranslentService translent02,
                ISingeltonService singelton01,
                    ISingeltonService singelton02

            )
        {
            _logger = logger;
            this.scoped01 = scoped01;
            this.scoped02 = scoped02;
            this.translent01 = translent01;
            this.translent02 = translent02;
            this.singelton01 = singelton01;
            this.singelton02 = singelton02;
        }

        //get //Home/TestLifeTime
        public string TestLifeTime()
        {
             StringBuilder builder = new StringBuilder();
            builder.Append($"scoped01:: {scoped01.GetGuid()}\n");
            builder.Append($"scoped02:: {scoped02.GetGuid()}\n\n");

            builder.Append($"translent01:: {translent01.GetGuid()}\n");
            builder.Append($"translent02:: {translent02.GetGuid()}\n\n");

            builder.Append($"singelton01:: {singelton01.GetGuid()}\n");
            builder.Append($"singelton02:: {singelton02.GetGuid()}\n\n");

            return builder.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
