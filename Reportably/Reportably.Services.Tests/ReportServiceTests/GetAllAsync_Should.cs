using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Entities;
using Reportably.Services.Implementations;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Tests.ReportServiceTests
{
    [TestClass]
    public class GetAllAsync_Should
    {
        [TestMethod]
        public async Task ReturnAllReports()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAllReports));

            using (var actContext = new ReportablyDbContext(options))
            {
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());

                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var reports = await SUT.GetAllAsync(new CancellationToken());

                Assert.AreEqual(3, reports.Count());
            }
        }

        [TestMethod]
        public async Task ReturnIReadOnlyCollectionWithReport()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnIReadOnlyCollectionWithReport));
           
            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var reports = await SUT.GetAllAsync(new CancellationToken());

                Assert.IsInstanceOfType(reports, typeof(IReadOnlyCollection<Report>));
            }
        }
    }
}
