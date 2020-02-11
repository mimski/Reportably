using Reportably.Services.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Contracts
{
    public interface IUserService
    {
        Task<UserModel> FindByIdAsync(string id, CancellationToken cancellationToken);
    }
}
