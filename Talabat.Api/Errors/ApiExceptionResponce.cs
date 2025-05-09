﻿namespace Talabat.Api.Errors
{
    public class ApiExceptionResponce: ApiResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponce(int statusCode,string? message=null,string? details=null):base(statusCode, message )
        {
            Details=details;
        }
    }
}
