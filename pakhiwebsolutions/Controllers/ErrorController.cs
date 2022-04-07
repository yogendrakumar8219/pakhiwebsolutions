﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FeesManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
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
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    logger.LogWarning($"404 error occured. Path= {statusCodeResult.OriginalPath}" +
                        $"and QueryString = {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //LogError() method logs the exception under Error categoy in the log
            logger.LogError($"The path {exceptionHandlerPathFeature.Path}" + $" threw an excepation {exceptionHandlerPathFeature.Error}");
            /*            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
                        ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
                        ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;*/
            return View("Error");
        }

    }
}
