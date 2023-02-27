using Microsoft.AspNetCore.Authorization;

namespace SpaceManagment.Common
{
    public class CustomAuthenticate 
    {
        internal class MyAuthenticate : AuthorizeAttribute
        {
            const string policyName = $"{AuthorazationAttributes.WithoutAuthorization}";
            public MyAuthenticate() => Policy = policyName;
        }
    }
}
