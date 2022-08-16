namespace Museums.Api.Helpers
{
    public static class HttpContextExtensions
    {
        public static void AddHeaderTotalRecords(this HttpContext httpContext, int totalRecords)
        {
            if (httpContext is null)
                throw new ArgumentException(nameof(httpContext));

            httpContext.Response.Headers.Add("TotalRecords", totalRecords.ToString());
        }
        public static void AddHeaderTotalRecordsFiltered(this HttpContext httpContext, int totalRecordsFiltered)
        {
            if (httpContext is null)
                throw new ArgumentException(nameof(httpContext));

            httpContext.Response.Headers.Add("TotalRecordsFiltered", totalRecordsFiltered.ToString());
        }
    }

}