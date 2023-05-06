using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.ViewModels.Common
{
    public class APIErrorResult<T> : APIResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public APIErrorResult()
        {
        }

        public APIErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public APIErrorResult(string[] validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}
