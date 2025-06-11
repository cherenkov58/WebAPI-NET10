using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Authority;

namespace WebAPIDemo.Filters.AuthFilters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //1. Get Authorization header from the request
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string tokenString = authorizationHeader.ToString();

            //2. Get rid of the Bearer prefix
            if (tokenString.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                tokenString = tokenString.Substring("Bearer ".Length).Trim();
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //3. Get Configuration and the Secret Key
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var securityKey = configuration?["SecurityKey"]??string.Empty;

            //4. Verify the token
            if (!await Authenticator.VerifyTokenAsync(tokenString, securityKey))
            {
                context.Result = new UnauthorizedResult();
            }            
        }
    }
}
