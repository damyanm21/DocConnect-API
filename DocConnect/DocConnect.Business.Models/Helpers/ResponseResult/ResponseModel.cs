using System.Text.Json;

namespace DocConnect.Business.Models.Helpers.ResponseResult
{
    /// <summary>
    /// The Response Model class is used for the responses by the API. It holds information whether the request was successfully finished or not. 
    /// </summary>
    public class ResponseModel
    {
        public const string DefaultErrorMessage = "Something went wrong!";
        public bool Success { get; set; }
        public int HttpStatusCode { get; set; }
        public object? ErrorMessage { get; set; }
        public object? Result { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            });
        }
    }
}
