using Newtonsoft.Json;
using System.Text;

namespace Museums.Api.Helpers
{   
    /// <summary>
    /// Add customer headers, author and source info
    /// </summary>
    public class HeadersMiddleware
    {
        private RequestDelegate _next;

        public HeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Author", "Victor Martinez");
            context.Response.Headers.Add("source-info", " Sistema de informacion de Cultura/Secretaria de cultura https://sic.cultura.gob.mx");

            await _next(context);
        }

    }

}