using Microsoft.EntityFrameworkCore;
using Reportably.Services.Contracts;
using Reportably.Services.Mappings.Extensions;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ReportablyDbContext context;

        public UserService(ReportablyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserModel> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            var user = await this.context.Users.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

            return user.ToService(); ;
        }
    }
}
