using Microsoft.EntityFrameworkCore;
using Reportably.Web.Data;

namespace Reportably.Services.Tests
{
    public static class TestUtilities
    {
        public static DbContextOptions<ReportablyDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ReportablyDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
