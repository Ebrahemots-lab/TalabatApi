public class ExceptionHandlingMiddlware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddlware> _logger;

    public ExceptionHandlingMiddlware(RequestDelegate next , ILogger<ExceptionHandlingMiddlware> logger)
    {
        _next = next; //represent the next middlware in the pipeline
        _logger = logger; // used to log any exception
    }

    //This method will Talk to the next middlware and catch exception if happend
    public async Task InvokeAsync(HttpContext context)
    {
        //called when the http request is recived by application
        try
        {
            //try to go to the next middlware 
            await _next(context);
        }
        catch(Exception ex)
        {
            await  HandleExceptionAsync(context, ex);

        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
    //
        _logger.LogError(ex,"an error has been occured");

        var exception = new ApiExceptionResponse(500)
        {
            Message = "Sorry , an error has been occured",
            Details = ex.Message
        };


        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) exception.StatusCode;
        await context.Response.WriteAsJsonAsync(exception);
    }
}