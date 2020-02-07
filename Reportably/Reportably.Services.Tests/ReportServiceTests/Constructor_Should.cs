using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reportably.Services.Implementations;
using Reportably.Web.Data;
using System;

namespace Reportably.Services.Tests.ReportServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowsArgumentNullException_WhenPassedContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ReportService(null));
        }

        [TestMethod]
        public void ReturnInstance()
        {
            var contextOptions = TestUtilities.GetOptions(nameof(ReturnInstance));

            using (var context = new ReportablyDbContext(contextOptions))
            {
                var SUT = new ReportService(context);

                Assert.IsNotNull(SUT);
                Assert.IsInstanceOfType(SUT, typeof(ReportService));
            }
        }
    }
}
