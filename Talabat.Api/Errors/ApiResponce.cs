

namespace Talabat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statuscode,string ?message=null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefaultResponce(statuscode);
            
        }

        private string? GetDefaultResponce(int statuscode)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                500 => "Server Error",
                401 => "Unauthorized",
                404 => "Not Found",
                _ => null
            };
        }
       
    }
}
