using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reportably.Services.Contracts;
using Reportably.Web.Areas.Reports.Mappings;
using Reportably.Web.Models;

namespace Reportably.Web.Areas.Reports.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.reportService.GetAllAsync(cancellationToken);

            return View("Index", result.ToViewModel());
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator, Librarian")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportViewModel reportViewModel, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
            {
                //throw new ArgumentException("Invalid input!");
                return this.View();
            }

            var report = await this.reportService.AddAsync(reportViewModel.ToServiceModel(), cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [HttpGet("/details")]
        [Route("{id}")]
        //[Authorize]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            var report = await this.reportService.FindByIdAsync(id, cancellationToken);

            if (report == null)
            {
                return NotFound();
            }

            return View("Details", report.ToViewModel());
        }

    }
}