using Mvc.Models.Abstract;

namespace Mvc.Models
{
    public class ApiResponseModel<T> : IApiResponseModel
    {
        public T data { get; set; }
        public bool success { get ; set; }
        public string message { get ; set; }
    }
}
