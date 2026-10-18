using System.Net;

namespace VasilevaAnnaKT_42_23.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger) 
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception", ex);
                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";
                var responseModel = new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = ex.Message,
                };

                switch (ex)
                {
                    default:
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Errors =new List<string>() { ex.InnerException?.Message };
                        break;
                }
                await httpResponse.WriteAsJsonAsync(responseModel);
            }
        }
    }
    public class ResponseModel<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public ResponseModel() { }
        public ResponseModel(T data, string mes = null) 
        {
            Succeeded = true;
            Message = mes;
            Data = data;
        }

        public ResponseModel(string mes)
        {
            Succeeded = true;
            Message = mes;
        }
    }
}
