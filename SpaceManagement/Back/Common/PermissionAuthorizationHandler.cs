using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SpaceManagment.Common;
using SpaceManagment.Model;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    UserManager<User> _userManager;
    RoleManager<Role> _roleManager;
    IHttpContextAccessor _httpContextAccessor = null;

    public PermissionAuthorizationHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userName = context.User.Claims.Where(e => e.Type == "UserName").FirstOrDefault()?.Value;
        //if (context.User.Identity.Name == null)
        //{
        //    return;
        //}
        if (userName == null)
        {
            return;
        }
        if (requirement.Permission == AuthorazationAttributes.WithoutAuthorization)
        {
            foreach(var req in context.PendingRequirements)
            {
                context.Succeed(req);
            }
            return;
        }
        var area = _httpContextAccessor.HttpContext.Request.RouteValues["area"]?.ToString() ?? string.Empty;
        var controller = _httpContextAccessor.HttpContext.Request.RouteValues["controller"]?.ToString() ?? "";
        var action = _httpContextAccessor.HttpContext.Request.RouteValues["action"]?.ToString() ?? "";
        if (string.IsNullOrEmpty(area))
        {
            requirement.Permission = $"{controller}.{action}";
        }
        else
        {
            requirement.Permission = $"{area}.{controller}.{action}";
        }

       
        // Get all the roles the user belongs to and check if any of the roles has the permission required
        // for the authorization to succeed.
        var user = await _userManager.FindByNameAsync(userName);
        var userRoleNames = await _userManager.GetRolesAsync(user);
        var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

        foreach (var role in userRoles)
        {
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                    x.Value == requirement.Permission)
                                        //x.Issuer == "LOCAL AUTHORITY")
                                        .Select(x => x.Value);

            if (permissions.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
