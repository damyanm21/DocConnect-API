using System.Net;

namespace DocConnect.Business.Models.Helpers.ResponseResult
{
    /// <summary>
    /// The Http Response Helper is used to create Response model objects depending on if the requested action was successfull or not.
    /// </summary>
    public static class HttpResponseHelper
    {
        /// <summary>
        /// Creates Response Model with Success status = true, representing a successfull request.
        /// </summary>
        /// <param name="status">The http status code of the request</param>
        /// <param name="result">The result needed to be returned by the request. Null by default</param>
        /// <returns></returns>
        public static ResponseModel Success(HttpStatusCode status, object? result = null)
        {
            return new ResponseModel
            {
                Success = true,
                HttpStatusCode = (int)status,
                Result = result
            };
        }

        /// <summary>
        /// Creates Response Model with Success status = false, representing a failed request.
        /// </summary>
        /// <param name="status">The http status code of the request</param>
        /// <param name="errorMessage">The error message/messages that caused the request to fail. Null by default</param>
        /// <returns></returns>
        public static ResponseModel Error(HttpStatusCode status, object? errorMessage = null)
        {
            return new ResponseModel
            {
                Success = false,
                HttpStatusCode = (int)status,
                ErrorMessage = errorMessage ?? ResponseModel.DefaultErrorMessage
            };
        }
    }
}
