public class ApiValidationErrorResponse : ApiBaseError
{
    public IEnumerable<string> Errors {get;set;}
    public ApiValidationErrorResponse() : base(400)
    {
    }
}