using Reportably.Services.Models;
using Reportably.Web.Models;

namespace Reportably.Web.Mappings
{
    internal static class ReportSystemMappings
    {
        public static ReportSystemViewModel ToViewModel(this ReportSystem entity)
        {
            return entity != null ? new ReportSystemViewModel
            {
                TotalReports = entity.TotalReports
            } : null;
        }
    }
}
