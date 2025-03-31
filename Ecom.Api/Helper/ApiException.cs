namespace Ecom.Api.Helper
{
    public class ApiException : ResponseApi
    {
        public string Details { get; set; }
        public ApiException(int statusCode, string message, string details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
