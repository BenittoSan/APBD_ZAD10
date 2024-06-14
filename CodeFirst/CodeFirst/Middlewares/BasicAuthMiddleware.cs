using System.Net.Http.Headers;
using System.Text;

namespace CodeFirst.Middlewares;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
   

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
        
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);

            if (authHeader.Scheme.Equals("basic",StringComparison.OrdinalIgnoreCase) && 
                authHeader.Parameter != null)
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var userName = credentials[0];
                var password = credentials[1];

                if (IsAuthorized(userName,password))
                {
                    await _next(context);
                    return;
                }

            }
        }

     
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    private bool IsAuthorized(string userName, string passoword)
    {
        return userName == "beny" && passoword == "admin";
    }
}