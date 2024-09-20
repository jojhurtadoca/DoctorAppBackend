namespace API.Errors
{
    public class ApiErrorResponse
    { 
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public ApiErrorResponse(int statusCode, string errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetStatusCodeMessage(statusCode);
        }

        private string GetStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Internal Server Error",
            };
        }
    }
}
