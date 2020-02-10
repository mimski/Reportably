using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Entities;
using Reportably.Services.Implementations;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Tests.ReportServiceTests
{
    [TestClass]
    public class FindByIdAsync_Should
    {
        [TestMethod]
        public async Task ReturnReportInstance_WhenReportExistInDb()
        {
            var testReport = new Report();
            var testReport2 = new Report();
            var testReport3 = new Report();
            var cancellationToken = new CancellationToken();

            var options = TestUtilities.GetOptions(nameof(ReturnReportInstance_WhenReportExistInDb));

            ReportEntity report;

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(testReport, cancellationToken);
                await SUT.AddAsync(testReport2, cancellationToken);

                await actContext.SaveChangesAsync();

                report = await actContext.Reports.FirstAsync();
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var result = await SUT.FindByIdAsync(report.Id,cancellationToken);

                Assert.IsInstanceOfType(result, typeof(Report));
            }
        }

        [TestMethod]
        public async Task ReturnNull_WhenReportDoesNotExistInDb()
        {
            var testReport = new Report();
            var testReport2 = new Report();
            var testReport3 = new Report();
            var cancellationToken = new CancellationToken();

            var options = TestUtilities.GetOptions(nameof(ReturnNull_WhenReportDoesNotExistInDb));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var result = await SUT.FindByIdAsync(new Guid(), cancellationToken);

                Assert.IsTrue(result == null);
            }
        }
    }
}
