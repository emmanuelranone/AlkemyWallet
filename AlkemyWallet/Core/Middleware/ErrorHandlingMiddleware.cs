using Newtonsoft.Json;
using System.Net;

namespace AlkemyWallet.Core.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync
                (JsonConvert.SerializeObject
                (new 
                { 
                    status = (int)HttpStatusCode.BadRequest, 
                    errors = new[] { new {  error = ex.Message } } 
                }));
        }
    }
}
