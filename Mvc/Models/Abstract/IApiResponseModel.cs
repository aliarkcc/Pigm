using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models.Abstract
{
    public interface IApiResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
