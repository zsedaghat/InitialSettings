using Microsoft.AspNetCore.Authorization;

namespace SpaceManagment.Common
{
    public class CustomAuthorize
    {
        internal class MyAuthorize : AuthorizeAttribute
        {
            const string policyName = "default";
            public MyAuthorize() => Policy = policyName;

        }
    }
}
