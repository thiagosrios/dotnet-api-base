using ApiBase.Exceptions;
using ApiBase.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiBaseTest.UnitTests.Filters
{
    [TestClass]
    public class ExceptionFilterTest
    {
        private ActionContext GetActionContextMock()
        {
            ActionContext actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor(),
            };

            return actionContext;
        }

        private string GetResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            StreamReader stream = new StreamReader(response.Body);
            string body = stream.ReadToEnd();
            stream.Close();

            return body;
        }

        [TestMethod]
        public void TestExceptionFilter()
        {
            ActionContext context = this.GetActionContextMock();
            IList<IFilterMetadata> filters = new List<IFilterMetadata>();
            ExceptionContext exceptionContext = new ExceptionContext(context, filters)
            {
                Exception = new BusinessException("Erro")
            };

            ExceptionFilter ex = new ExceptionFilter();
            ex.OnException(exceptionContext);

            HttpResponse response = exceptionContext.HttpContext.Response;
            string body = GetResponseBody(response);

            Assert.IsNotNull(ex);
            Assert.IsNotNull(response);
            Assert.IsNotNull(body);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void TestExceptionFilterNotImplementedException()
        {
            ActionContext context = this.GetActionContextMock();
            IList<IFilterMetadata> filters = new List<IFilterMetadata>();
            ExceptionContext exceptionContext = new ExceptionContext(context, filters)
            {
                Exception = new NotImplementedException()
            };

            ExceptionFilter ex = new ExceptionFilter();
            ex.OnException(exceptionContext);

            HttpResponse response = exceptionContext.HttpContext.Response;
            string body = GetResponseBody(response);

            Assert.IsNotNull(ex);
            Assert.IsNotNull(response);
            Assert.IsNotNull(body);
            Assert.AreEqual(501, response.StatusCode);
        }

        [TestMethod]
        public void TestExceptionFilterUnauthorizedAccessException()
        {
            ActionContext context = this.GetActionContextMock();
            IList<IFilterMetadata> filters = new List<IFilterMetadata>();
            ExceptionContext exceptionContext = new ExceptionContext(context, filters)
            {
                Exception = new UnauthorizedAccessException()
            };

            ExceptionFilter ex = new ExceptionFilter();
            ex.OnException(exceptionContext);

            HttpResponse response = exceptionContext.HttpContext.Response;
            string body = GetResponseBody(response);

            Assert.IsNotNull(ex);
            Assert.IsNotNull(response);
            Assert.IsNotNull(body);
            Assert.AreEqual(401, response.StatusCode);
        }
    }
}
