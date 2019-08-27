using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace UserApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private ILogger<GlobalExceptionFilter> logger;
        public readonly IHostingEnvironment hosting;
        public GlobalExceptionFilter(IHostingEnvironment hosting, ILogger<GlobalExceptionFilter> logger)
        {
            this.hosting = hosting;
            this.logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var json = new JsonErrorExpection ();
            if (context.Exception.GetType()==typeof(UserOperatorExpection))
            {
                json.Message = context.Exception.Message;
                context.Result = new BadRequestObjectResult(json);
            }
         
            else
            {
                json.Message = "发生了未知内部错误";
                if (hosting.IsDevelopment())
                {
                    json.DevelopMessage = context.Exception.StackTrace;
                }
                context.Result = new InternalServerErrorObjectResult(json);
            }

            logger.LogError(context.Exception,context.Exception.Message);
        }
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error):base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
