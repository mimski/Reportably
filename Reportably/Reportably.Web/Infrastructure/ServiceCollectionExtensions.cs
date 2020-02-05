using Microsoft.Extensions.DependencyInjection;
using Reportably.Services.Contracts;
using Reportably.Services.Implementations;

namespace Reportably.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}
