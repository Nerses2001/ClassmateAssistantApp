using Entity;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using Newtonsoft.Json;

namespace ClassmateAssistantApp.Middleware
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext.Request.Body.CanSeek)
                context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            var requestModel = JsonConvert.SerializeObject(new RequestModel
            {
                Body = HttpContextHelper.DeserializeFromStream(context.HttpContext.Request.Body),
                QueryParams = context.HttpContext.Request.QueryString.ToString(),
                IPAddress = HttpContextHelper.GetClientIpAddress(context.HttpContext),
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path.ToString()
            });

            var username = "Anonymous";

            if (context.HttpContext.Items["User"] != null)
            {
                var user = (ApplicationUser)context.HttpContext.Items["User"]!;

                username = user.UserName;
            }

            var problemDetails = new ErrorResponse();
            var service = "UNKNOWN";
            var reasonText = "InternalServerEror";


            problemDetails.Title = "Internal server error";
            problemDetails.Message = "Internal server error, please try again later.";
            problemDetails.Type = "InternalServerException";

            context.Result = new InternalServerErrorObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;


            var problemDetailsLog = new ErrorLog
            {
                Title = problemDetails.Title,
                Message = context.Exception.Message,
                Type = problemDetails.Type,
                Reason = reasonText,
                StackTrace = context.Exception.StackTrace
            };
            context.ExceptionHandled = true;
        }
    }
}
