namespace Ecom.Api.Helper
{
    public class ResponseApi
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public ResponseApi(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageWithStatusCode(statusCode);
        }
        private string GetMessageWithStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => " Server Error",
                429 => " Too Many Request",
                _ => "Null"
            };
        }
    }
}
