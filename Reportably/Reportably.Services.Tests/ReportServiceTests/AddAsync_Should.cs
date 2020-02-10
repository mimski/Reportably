using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Services.Implementations;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Tests.ReportServiceTests
{
    [TestClass]
    public class AddAsync_Should
    {
        [TestMethod]
        public async Task SaveReportInDatabase()
        {
            var options = TestUtilities.GetOptions(nameof(SaveReportInDatabase));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(new Report(), new CancellationToken());
            }

            using (var assertContext = new ReportablyDbContext(options))
            {
                Assert.AreEqual(1, assertContext.Reports.Count());
            }
        }

        [TestMethod]
        public async Task SetPassedTitleValueOfReport()
        {
            var testTitle = "Test Title";

            var testReport = new Report()
            {
                Title = testTitle,
            };

            var options = TestUtilities.GetOptions(nameof(SetPassedTitleValueOfReport));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(testReport, new CancellationToken());
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var report = await assertContext.Reports.FirstOrDefaultAsync();

                Assert.AreEqual(testTitle, report.Title);
            }
        }

        [TestMethod]
        public async Task SetPassedSummaryValueOfReport()
        {
            var testSummary = "Test Summary";

            var testReport = new Report()
            {
                Summary = testSummary,
            };

            var options = TestUtilities.GetOptions(nameof(SetPassedSummaryValueOfReport));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(testReport, new CancellationToken());
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var report = await assertContext.Reports.FirstOrDefaultAsync();

                Assert.AreEqual(testSummary, report.Summary);
            }
        }

        [TestMethod]
        public async Task SetPassedAuthorValueOfReport()
        {
            var testAuthor = "Test Author Name";

            var testReport = new Report()
            {
                Author = testAuthor,
            };

            var options = TestUtilities.GetOptions(nameof(SetPassedAuthorValueOfReport));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(testReport, new CancellationToken());
            }

            using (var assertContext = new ReportablyDbContext(options))
            {
                var report = await assertContext.Reports.FirstOrDefaultAsync();

                Assert.AreEqual(testAuthor, report.Author);
            }
        }

        [TestMethod]
        public async Task SetPassedPublicationDateValueOfReport()
        {
            var testPublicationDate = new DateTime(2020,2,2);

            var testReport = new Report()
            {
                PublicationDate = testPublicationDate,
            };

            var options = TestUtilities.GetOptions(nameof(SetPassedPublicationDateValueOfReport));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(testReport, new CancellationToken());
            }

            using (var assertContext = new ReportablyDbContext(options))
            {
                var report = await assertContext.Reports.FirstOrDefaultAsync();

                Assert.AreEqual(testPublicationDate, report.PublicationDate);
            }
        }

        [TestMethod]
        public async Task ReturnReportInstance()
        {
            var testReport = new Report();

            var options = TestUtilities.GetOptions(nameof(ReturnReportInstance));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                var result = await SUT.AddAsync(testReport, new CancellationToken());

                Assert.IsInstanceOfType(result, typeof(Report));
            }
        }
    }
}
