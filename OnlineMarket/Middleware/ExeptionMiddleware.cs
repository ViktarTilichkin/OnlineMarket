using System.Net;

namespace OnlineMarket.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate m_Next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            m_Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await m_Next(context);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionWithCodeAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {

                await HandleExceptionWithCodeAsync(context, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private Task HandleExceptionWithCodeAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(message);
        }
    }
}
