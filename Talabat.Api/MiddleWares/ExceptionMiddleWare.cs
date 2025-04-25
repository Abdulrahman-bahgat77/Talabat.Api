using System.Net;
using System.Text.Json;
using Talabat.Api.Errors;

namespace Talabat.Api.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
           _env = env;
        }
        //InvokedAsync

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //if (_env.IsDevelopment())
                //{
                //    var Responce = new ApiExceptionResponce(500, ex.Message, ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Responce = new ApiExceptionResponce( (int)HttpStatusCode.InternalServerError);
                //}
                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var Responce = _env.IsDevelopment() ? new ApiExceptionResponce(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponce((int)HttpStatusCode.InternalServerError);
                var jsonResponce = JsonSerializer.Serialize(Responce);
                context.Response.WriteAsync(jsonResponce);
            }
        }


    }
}
