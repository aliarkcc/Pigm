using System.Collections.Generic;

namespace Mvc.Models
{
    public class ApiResponseModel<T>
    {
        public List<T> data{ get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}