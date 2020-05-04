using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Specialized;
using System.IO;

namespace Test.Utilities.Mocks
{
    public static class RequestMock
    {
        public static ControllerContext MockUploadContext(NameValueCollection formVaues)
        {
            // Conteúdo da request
            byte[] content = new byte[] { };

            // Mock da requisição
            Mock<HttpRequest> request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Form).Returns(formVaues as IFormCollection);
            request.SetupGet(x => x.Method).Returns(HttpMethods.Post);
            request.SetupGet(x => x.Body).Returns(new MemoryStream(content));
            request.SetupGet(x => x.ContentLength).Returns(content.Length);
            request.SetupGet(x => x.ContentType).Returns("multipart/form-data");

            // Mock do contexto http e configuração do contexto do controller
            Mock<HttpContext> httpContext = new Mock<HttpContext>(MockBehavior.Strict);
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext.Object
            };

            return controllerContext;
        }
    }
}
