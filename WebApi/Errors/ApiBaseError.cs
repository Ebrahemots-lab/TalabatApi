public class ApiBaseError
{

    public int StatusCode { get; set; }

    public string? Message { get; set; }

    public ApiBaseError(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GenerateMessageBasedOnCode(statusCode);
    }


    private string GenerateMessageBasedOnCode(int statusCode)
    {
        string message = statusCode switch
        {
            400 => "Bad Request",
            404 => "Not Found",
            500 => "Server Error",
            401 => "Not Authorized",
            _ => null
        };

        return message;
    }


}