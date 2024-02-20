using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tavernkeep.Application.Extensions;
using Tavernkeep.Core.Contracts.Authentication;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Server.Middleware
{
    /// <summary>
    /// Specifies that the class or method that this attribute is applied to requires the specific role.
    /// </summary>
    /// <param name="requiredRole">The required user role.</param>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresRoleAttribute(UserRole requiredRole) : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Performs request authorization.
        /// </summary>
        /// <param name="context">The <see cref="AuthorizationFilterContext"/> for the request.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roleClaim = context.HttpContext.User?.FindFirst(JwtCustomClaimNames.UserRole);

            if (roleClaim == null || roleClaim.Value.ToUserRole().IsInsufficient(requiredRole))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
