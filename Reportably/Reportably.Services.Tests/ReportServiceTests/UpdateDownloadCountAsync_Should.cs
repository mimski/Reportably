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
            ulong testDownloadCount = 1;
            var testReport = new ReportEntity()
            {
                DownloadCount = testDownloadCount
            };

            var options = TestUtilities.GetOptions(nameof(IncrementDownloadCountWithOne));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await actContext.Reports.AddAsync(testReport);
                await actContext.SaveChangesAsync();

                var report = await actContext.Reports.FirstOrDefaultAsync();

                var a = await SUT.UpdateDownloadCountAsync(report.Id, new CancellationToken());
                Assert.IsTrue(true == a);
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
                var SUT = new ReportService(actContext);

                await actContext.Reports.AddAsync(new ReportEntity());
                await actContext.SaveChangesAsync();

                var report = await actContext.Reports.FirstOrDefaultAsync();

                var result = await SUT.UpdateDownloadCountAsync(report.Id, new CancellationToken());
                Assert.IsTrue(true == result);
            }
        }

        [TestMethod]
        public async Task ReturnFalse_WhenReportWithPassedReportIdDoesNotExistInDb()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_WhenReportWithPassedReportIdDoesNotExistInDb));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                Guid reportId = new Guid("CD1D4E6B-D86D-4AA1-2661-08D7AE3670FB");

                var result = await SUT.UpdateDownloadCountAsync(reportId, new CancellationToken());
                Assert.IsTrue(false == result);
            }
        }
    }
}
