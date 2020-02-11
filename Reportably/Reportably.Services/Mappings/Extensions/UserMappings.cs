using Reportably.Entities;
using Reportably.Services.Models;

namespace Reportably.Services.Mappings.Extensions
{
    internal static class UserMappings
    {
        public static UserModel ToService(this User entity)
        {
            return entity != null
                ? new UserModel
                {
                    IsEmailConfirmed = entity.EmailConfirmed
                }
                : null;
        }
    }
}
