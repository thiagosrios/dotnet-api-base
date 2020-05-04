using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;

namespace ApiBase.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status;
            var exceptionType = context.Exception.GetType();
            string message;

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Acesso não autorizado";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "Ocorreu um erro no servidor";
                status = HttpStatusCode.NotImplemented;
            }
            else
            {
                message = context.Exception.Message;
                status = HttpStatusCode.NotFound;
            }
            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            string json = JsonConvert.SerializeObject(new { title = "Erro", status, message });
            response.WriteAsync(json);
        }
    }
}
