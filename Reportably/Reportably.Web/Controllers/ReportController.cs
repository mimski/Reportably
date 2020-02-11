using System;
using System.Security.Claims;
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
        private readonly IUploadedFileService uploadedFileService;
        private readonly IUserService userService;

        public ReportController(IReportService reportService, IUploadedFileService uploadedFileService, IUserService userService)
        {
            this.reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            this.uploadedFileService = uploadedFileService ?? throw new ArgumentNullException(nameof(uploadedFileService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService)); ;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await this.reportService.GetAllAsync(cancellationToken);

            return View("Index", result.ToViewModel());
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportViewModel reportViewModel, CancellationToken cancellationToken)
        {
            if (!this.ModelState.IsValid)
            {
                //throw new ArgumentException("Invalid input!");
                return this.View();
            }
            if (reportViewModel.File.ContentType == null || reportViewModel.File.ContentType != "application/pdf")
            {
                this.ModelState.AddModelError(string.Empty, "Please upload .pdf file");
            }

            var report = await this.reportService.AddAsync(reportViewModel.ToServiceModel(), cancellationToken);
            await this.uploadedFileService.AddAsync(reportViewModel.File, report.Id, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [HttpGet("/details")]
        [Route("{id}")]
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
        [Authorize]
        public async Task<ActionResult> FileDownload(Guid reportId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedUser = await this.userService.FindByIdAsync(userId, cancellationToken);

            if (!loggedUser.IsEmailConfirmed)
            {
                return RedirectToAction("Index", new { ReturnUrl = "~/Account/LogOff" });

                //return RedirectToAction();

                //HttpContext.Server.TransferRequest("~/Account/LogOff");
                //return RedirectToAction("Login");
                //return LocalRedirect("~/Account/ConfirmEmail");
                // return RedirectToAction("Index");
                //if the user's email is not confirmed, return to the view page that inform that the user hasn't confirmed his email
            }
            else
            {
                var report = await this.uploadedFileService.GetFileAsync(reportId, cancellationToken);

                await this.reportService.UpdateDownloadCountAsync(reportId, cancellationToken);

                //return File(report.FileContent, report.ContentType, true); // open the file
                return File(report.FileContent, System.Net.Mime.MediaTypeNames.Application.Octet, "report.pdf");
            }
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel searchViewModel)
        {
            string title = searchViewModel.Title;
            string summary = searchViewModel.Summary;
            string author = searchViewModel.Author;
            string option;
            if (searchViewModel.Option != null)
            {
                option = searchViewModel.Option;
            }
            else
            {
                option = "and";
            }
            var result = await this.reportService.Search(title, summary, author, option);

            return PartialView("_SearchResult", result.ToViewModel());
        }
    }
}