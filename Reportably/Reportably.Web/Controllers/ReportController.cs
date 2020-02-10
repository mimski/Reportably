using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reportably.Services.Contracts;
using Reportably.Web.Areas.Reports.Mappings;
using Reportably.Web.Models;

namespace Reportably.Web.Areas.Reports.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;
        private readonly IUploadedFileService uploadedFileService;

        public ReportController(IReportService reportService, IUploadedFileService uploadedFileService)
        {
            this.reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            this.uploadedFileService = uploadedFileService ?? throw new ArgumentNullException(nameof(uploadedFileService));
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
            if (reportViewModel.File.ContentType != "application/pdf")
            {
                this.ModelState.AddModelError(string.Empty, "Please upload only .pdf file");
            }
            if (!this.ModelState.IsValid)
            {
              
                //throw new ArgumentException("Invalid input!");
                return this.View();
            }

            var report = await this.reportService.AddAsync(reportViewModel.ToServiceModel(), cancellationToken);
            await this.uploadedFileService.AddAsync(reportViewModel.File, report.Id, cancellationToken);

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

        [HttpGet]
        //[Authorize]
        public async Task<FileResult> FileDownload(Guid reportId, CancellationToken cancellationToken)
        {
            // current session user to check property for email verification is true
            // Security - if current != 
            //if (HttpContext.)
            //{
            // badquest
            //}


            var report = await this.uploadedFileService.GetFileAsync(reportId, cancellationToken);
            //BusinessLayer.GetDocumentsByDocument(documentId, AuthenticationHandler.HostProtocol).FirstOrDefault();

            await this.reportService.UpdateDownloadCount(reportId, cancellationToken);

            return File(report.FileContent, report.ContentType, true);
        }
    }
}