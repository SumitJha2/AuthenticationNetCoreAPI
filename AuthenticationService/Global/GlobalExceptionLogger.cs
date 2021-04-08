using AuthenticationService.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService
{
    public class GlobalExceptionLogger : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var error = new ErrorDetails()
            {
                StatusCode = 500,
                Message = "Something went wrong! Internal Server Error."
            };
            // need to write the exception to the file.
            // not implementing yet(logger)-
            context.Result = new JsonResult(error);
            return Task.CompletedTask;
        }
    }
}
