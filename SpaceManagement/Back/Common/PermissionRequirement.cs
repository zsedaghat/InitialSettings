﻿using Microsoft.AspNetCore.Authorization;

namespace SpaceManagment.Common
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get;  set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
