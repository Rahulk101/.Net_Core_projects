using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Filters
{
    public class RateExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log exception
            if(context.Exception is TimeoutException)
            {
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}
