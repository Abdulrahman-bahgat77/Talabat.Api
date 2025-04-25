namespace Talabat.Api.Errors
{
    public class ApiValidationErrorResponce:ApiResponse
    {
        public IEnumerable<string> Error { get; set; }
        public ApiValidationErrorResponce():base(400)
        {
            Error = new List<string>();
        }
    }
}
