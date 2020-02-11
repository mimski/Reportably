using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Entities;
using Reportably.Services.Implementations;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Tests.ReportServiceTests
{
    [TestClass]
    public class GetTotalReportsbCountAsync_Should
    {
        [TestMethod]
        public async Task ReturnTotalReportsCount()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTotalReportsCount));

            using (var actContext = new ReportablyDbContext(options))
            {
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());

                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var result = await SUT.GetTotalReportsbCountAsync(new CancellationToken());

                Assert.AreEqual(6, result.TotalReports);
            }
        }

        [TestMethod]
        public async Task ReturnIstanceOfReportSystem()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnIstanceOfReportSystem));

            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var reports = await SUT.GetTotalReportsbCountAsync(new CancellationToken());

                Assert.IsInstanceOfType(reports, typeof(ReportSystem));
            }
        }
    }
}
