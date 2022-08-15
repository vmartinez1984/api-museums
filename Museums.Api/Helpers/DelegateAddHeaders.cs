using System.Text;

namespace Museums.Api.Helpers
{
    public class ExampleMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ExampleMiddleware> _logger;

        public ExampleMiddleware(
            RequestDelegate next,
            ILogger<ExampleMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        private async Task AnalizeBody(HttpContext context)
        {
            using (StreamReader stream = new StreamReader(context.Request.Body))
            {
                string body = await stream.ReadToEndAsync();
                _logger.LogInformation("Method: " + context.Request.Method);
                if (string.IsNullOrEmpty(body) == false)
                    _logger.LogInformation("Body: " + body);
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                context.Request.Body = new MemoryStream(bytes);
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Path: " + context.Request.Path);
            await AnalizeBody(context);
            // Store the original body stream for restoring the response body back to its original stream
            var originalBodyStream = context.Response.Body;

            // Create new memory stream for reading the response; Response body streams are write-only, therefore memory stream is needed here to read
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // Call the next middleware
            await _next(context);

            // Set stream pointer position to 0 before reading
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Read the body from the stream
            var responseBodyText = await new StreamReader(memoryStream).ReadToEndAsync();
            _logger.LogInformation(responseBodyText);
            // Reset the position to 0 after reading
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Do this last, that way you can ensure that the end results end up in the response.
            // (This resulting response may come either from the redirected route or other special routes if you have any redirection/re-execution involved in the middleware.)
            // This is very necessary. ASP.NET doesn't seem to like presenting the contents from the memory stream.
            // Therefore, the original stream provided by the ASP.NET Core engine needs to be swapped back.
            // Then write back from the previous memory stream to this original stream.
            // (The content is written in the memory stream at this point; it's just that the ASP.NET engine refuses to present the contents from the memory stream.)
            context.Response.Body = originalBodyStream;
            await context.Response.Body.WriteAsync(memoryStream.ToArray());

            // Per @Necip Sunmaz's recommendation this also works:
            // Just make sure that the memoryStrream's pointer position is set back to 0 again.
            // await memoryStream.CopyToAsync(originalBodyStream);
            // context.Response.Body = originalBodyStream;
        }
    }

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