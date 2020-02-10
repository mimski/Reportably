using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Entities;
using Reportably.Services.Implementations;
using Reportably.Web.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Tests.ReportServiceTests
{
    [TestClass]
    public class UpdateDownloadCountAsync_Should
    {
        [TestMethod]
        public async Task IncrementDownloadCountWithOne()
        {
            ulong testDownloadCount = 7;

            var testReport = new ReportEntity()
            {
                DownloadCount = testDownloadCount
            };

            var options = TestUtilities.GetOptions(nameof(IncrementDownloadCountWithOne));

            using (var actContext = new ReportablyDbContext(options))
            {
                actContext.Reports.Add(testReport);
                await actContext.SaveChangesAsync();

                var report = await actContext.Reports.FirstOrDefaultAsync();

                var SUT = new ReportService(actContext);
                await SUT.UpdateDownloadCountAsync(report.Id, new CancellationToken());
            }

            using (var assertContext = new ReportablyDbContext(options))
            {
                var report = await assertContext.Reports.FirstOrDefaultAsync();

                ulong expected = testDownloadCount + 1;

                Assert.AreEqual(expected, report.DownloadCount);
            }
        }

        [TestMethod]
        public async Task ReturnTrue_WhenReportWithPassedReportIdExistInDb()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTrue_WhenReportWithPassedReportIdExistInDb));

            using (var actContext = new ReportablyDbContext(options))
            {
                actContext.Reports.Add(new ReportEntity());
                await actContext.SaveChangesAsync();

                var report = await actContext.Reports.FirstOrDefaultAsync();

                var SUT = new ReportService(actContext);
                var result = await SUT.UpdateDownloadCountAsync(report.Id, new CancellationToken());

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task ReturnFalse_WhenReportWithPassedReportIdDoesNotExistInDb()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_WhenReportWithPassedReportIdDoesNotExistInDb));

            using (var actContext = new ReportablyDbContext(options))
            {
                Guid reportId = new Guid("CD1D4E6B-D86D-4AA1-2661-08D7AE3670FB");

                var SUT = new ReportService(actContext);
                var result = await SUT.UpdateDownloadCountAsync(reportId, new CancellationToken());

                Assert.IsFalse(result);
            }
        }
    }
}
