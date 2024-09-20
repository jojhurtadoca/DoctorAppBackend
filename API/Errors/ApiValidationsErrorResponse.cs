namespace API.Errors
{
    public class ApiValidationsErrorResponse: ApiErrorResponse
    {
        public ApiValidationsErrorResponse(): base(400)
        {

        }

        public IEnumerable<string> Errors { get; set; }
    }
}
