namespace OrderManagmentSystem.APIs.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int status, string msg = null, string details = null) : base(status, msg)
        {

            Details = details;
        }
    }
}
