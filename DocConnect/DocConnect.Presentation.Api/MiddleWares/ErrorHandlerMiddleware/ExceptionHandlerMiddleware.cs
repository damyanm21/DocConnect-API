using DocConnect.Business.Models.Helpers.ResponseResult;
using System.Net;
using static DocConnect.Business.Utilities.ErrorMessages;

namespace DocConnect.Presentation.Api.MiddleWares.ErrorHandlerMiddleware
{
    /// <summary>
    /// This is custom exception handler that catches exceptions thrown and returns a proper Response Model with  HttpStatusCode and error message.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

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
            catch (Exception exception)
            {
                _logger.LogError(exception, string.Format(ErrorOccurredAtRequest, context.Request.Path.Value));

                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = GetResponse(exception);

                response.StatusCode = responseModel.HttpStatusCode;

                await response.WriteAsync(responseModel.ToString());
            }
        }

        private ResponseModel GetResponse(Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.NotFound;
            object message = null;

            switch (exception)
            {
                case NullReferenceException:
                    message = NotFound;
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    message = CommonErrorMessage;
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            return HttpResponseHelper.Error(code, message);
        }

    }
}
