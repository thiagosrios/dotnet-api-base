using ApiBase.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ApiBaseTest.UnitTests.Filters
{
    [TestClass]
    public class ValidateModelFilterTest
    {
        [TestMethod]
        public void TestOnResulting()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            modelState.AddModelError("", "Campo inválido");

            ResultExecutingContext context = new ResultExecutingContext(
                new ActionContext(
                    httpContext: new DefaultHttpContext(),
                    routeData: new RouteData(),
                    actionDescriptor: new ActionDescriptor(),
                    modelState: modelState
                ),
                new List<IFilterMetadata>(),
                new Mock<ActionResult>().Object,
                new Mock<Controller>().Object
            );

            ValidateModelFilter filter = new ValidateModelFilter();
            filter.OnResultExecuting(context);

            Assert.IsNotNull(context.Result);
        }
    }
}
