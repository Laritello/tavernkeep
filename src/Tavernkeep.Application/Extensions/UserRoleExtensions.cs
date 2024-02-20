using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.Extensions
{
    public static class UserRoleExtensions
    {
        public static UserRole ToUserRole(this string roleName)
        {
            if (!Enum.TryParse(roleName, out UserRole role))
                role = UserRole.Player;

            return role;
        }

        public static bool IsSufficient(this UserRole role, UserRole requiredRole) => role >= requiredRole;

        public static bool IsInsufficient(this UserRole role, UserRole requiredRole) => role < requiredRole;
    }
}
