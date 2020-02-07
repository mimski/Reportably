using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Services.Implementations;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var testReport = new Report();
            var testReport2 = new Report();
            var testReport3 = new Report();
            var cancellationToken = new CancellationToken();

            var options = TestUtilities.GetOptions(nameof(ReturnAllReports));

            using (var actContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(actContext);

                await SUT.AddAsync(testReport, cancellationToken);
                await SUT.AddAsync(testReport2, cancellationToken);
                await SUT.AddAsync(testReport3, cancellationToken);

                await actContext.SaveChangesAsync();
            }
            using (var assertContext = new ReportablyDbContext(options))
            {
                var SUT = new ReportService(assertContext);
                var reports = await SUT.GetAllAsync(cancellationToken);

                Assert.AreEqual(3, reports.Count());
            }
        }
    }
}
