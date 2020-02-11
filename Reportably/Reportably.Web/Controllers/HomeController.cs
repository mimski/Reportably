using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reportably.Services.Contracts;
using Reportably.Web.Mappings;
using Reportably.Web.Models;

namespace Reportably.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReportService reportService;

        public HomeController(IReportService reportService)
        {
            this.reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.reportService.GetTotalReportsbCountAsync(cancellationToken);

            return View("Index", result.ToViewModel());
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
