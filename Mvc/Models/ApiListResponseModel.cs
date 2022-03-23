using Mvc.Models.Abstract;
using System.Collections.Generic;

namespace Mvc.Models
{
    public class ApiListResponseModel<T>:IApiResponseModel
    {
        public List<T> data{ get; set; }
        public bool success { get; set ; }
        public string message { get; set; }
    }
}