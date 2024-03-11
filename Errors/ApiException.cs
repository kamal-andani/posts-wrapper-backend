namespace zum_rails.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string error, string details)
        {
            StatusCode = statusCode;
            Error = error;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
    }
}
