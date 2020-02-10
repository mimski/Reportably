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
            var options = TestUtilities.GetOptions(nameof(ReturnReportInstance_WhenReportExistInDb));

            using (var actContext = new ReportablyDbContext(options))
            {
                actContext.Reports.Add(new ReportEntity());
                actContext.Reports.Add(new ReportEntity());

                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new ReportablyDbContext(options))
            {
                var report = await assertContext.Reports.FirstAsync();

                var SUT = new ReportService(assertContext);
                var result = await SUT.FindByIdAsync(report.Id, new CancellationToken());

                Assert.IsInstanceOfType(result, typeof(Report));
            }
        }

        [TestMethod]
        public async Task ReturnNull_WhenReportDoesNotExistInDb()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnNull_WhenReportDoesNotExistInDb));

            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var result = await SUT.FindByIdAsync(new Guid(), new CancellationToken());

                Assert.IsNull(result);
            }
        }
    }
}
