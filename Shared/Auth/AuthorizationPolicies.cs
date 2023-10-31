using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeAdmin", a => a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "Admin"));
        });
    }
}