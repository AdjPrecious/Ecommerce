using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace test.Controllers
{

    
    public class ErrorControlller : Controller
    {
        private readonly ILogger<ErrorControlller> logger;

        public ErrorControlller(ILogger<ErrorControlller> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resources you requested could not be found";
                    
                    
                    /*ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;*/
                    break;
            }
            return View("NotFound");
        }


        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
           /* logger.LogError($"The path {exceptionDetails.Path} threw an exception" +
                $" {exceptionDetails.Error}")*/
                
                ;
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMesssage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }

}
