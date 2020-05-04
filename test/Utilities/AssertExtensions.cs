using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiBaseTest.Utilities
{
    internal static class AssertExtensions
    {
        public static string VERIFICAR_RETORNO_NULO = "Retorno do método não deve ser nulo";

        public static void IsNotNullAndIsInt(this Assert assert, int result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(int).GetType(), "Tipo retornado deve ser do tipo int");
        }

        public static void IsNotNullAndIsBooleanTrue(this Assert assert, bool result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(bool).GetType(), "Tipo retornado deve ser do tipo bool");
            Assert.IsTrue(result, "Valor retornado deve ser igual a TRUE");
        }

        public static void IsNotNullAndIsBooleanFalse(this Assert assert, bool result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(bool).GetType(), "Tipo retornado deve ser do tipo bool");
            Assert.IsFalse(result, "Valor retornado deve ser igual a FALSE");
        }

        public static void IsNotNullAndIsDatetime(this Assert assert, object result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(DateTime).GetType(), "Tipo retornado deve ser DateTime");
        }

        public static void IsNotNullAndIsString(this Assert assert, object result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(string).GetType(), "Tipo retornado deve ser do tipo string");
        }

        public static void IsNotNullAndIsList<T>(this Assert assert, IList<T> result) where T : class
        {
            string messageType = string.Format("Valor retornado deve ser do tipo List<{0}>", result.GetType().Name);

            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsTrue(result.Any(), "Resultado da lista deve conter ao menos um registro");
            Assert.IsInstanceOfType(result.GetType(), typeof(IList<T>).GetType(), messageType);
        }

        public static void IsNotNullAndIsList<T>(this Assert assert, IEnumerable<T> result) where T : class
        {
            string messageType = string.Format("Valor retornado deve ser do tipo IEnumerable<{0}>", result.GetType().Name);

            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsTrue(result.Any(), "Resultado da lista deve conter ao menos um registro");
            Assert.IsInstanceOfType(result.GetType(), typeof(IEnumerable<T>).GetType(), messageType);
        }

        public static void ListIsEmpty<T>(this Assert assert, IList<T> result) where T : class
        {
            string messageType = string.Format("Valor retornado deve ser do tipo List<{0}>", result.GetType().Name);

            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsFalse(result.Any(), "Resultado da lista deve conter ao menos um registro");
            Assert.IsInstanceOfType(result.GetType(), typeof(IList<T>).GetType(), messageType);
        }

        public static void ListIsEmpty<T>(this Assert assert, IEnumerable<T> result) where T : class
        {
            string messageType = string.Format("Valor retornado deve ser do tipo IEnumerable<{0}>", result.GetType().Name);

            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsFalse(result.Any(), "Resultado da lista deve conter ao menos um registro");
            Assert.IsInstanceOfType(result.GetType(), typeof(IEnumerable<T>).GetType(), messageType);
        }

        public static void IsNotNullAndIsDecimalWithValue(this Assert assert, decimal result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(decimal).GetType(), "Tipo retornado deve ser decimal");
            Assert.IsTrue(result > 0, "Valor retornado deve ser maior que zero");
        }

        public static void IsNotNullAndIsDecimalZero(this Assert assert, decimal result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(decimal).GetType(), "Tipo retornado deve ser decimal");
            Assert.IsTrue(result <= 0, "Valor retornado deve ser menor igual a zero"); 
        }

        public static void IsNotNullAndIsLong(this Assert assert, object result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(long).GetType(), "Tipo retornado deve ser do tipo long");
        }

        public static void IsNotNullAndIsDouble(this Assert assert, object result)
        {
            Assert.IsNotNull(result, VERIFICAR_RETORNO_NULO);
            Assert.IsInstanceOfType(result.GetType(), typeof(double).GetType(), "Tipo retornado deve ser do tipo double");
        }

        public static void RequestIsSuccessful(this Assert assert, ActionResult result)
        {
            OkObjectResult action = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 200);
            Assert.IsInstanceOfType(result, expectedType: typeof(OkObjectResult));
        }

        public static void RequestIsSuccessful<T>(this Assert assert, ActionResult<T> result) where T : class
        {
            OkObjectResult action = result.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 200);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult<T>));
        }

        public static void RequestHasNoContent<T>(this Assert assert, ActionResult<T> result) where T : class
        {
            NoContentResult action = result.Result as NoContentResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 204);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult<T>));
        }

        public static void RequestHasNoContent(this Assert assert, ActionResult result)
        {
            NoContentResult action = result as NoContentResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 204);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult));
        }

        public static void RequestResourceNotFound<T>(this Assert assert, ActionResult<T> result) where T : class
        {
            NotFoundObjectResult action = result.Result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 404);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult<T>));
        }

        public static void RequestResourceNotFound(this Assert assert, ActionResult result)
        {
            NotFoundObjectResult action = result as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 404);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult));
        }

        public static void RequestFailed(this Assert assert, ActionResult result)
        {
            BadRequestObjectResult action = result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 400);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult));
        }

        public static void RequestFailed<T>(this Assert assert, ActionResult<T> result) where T : class
        {
            BadRequestObjectResult action = result.Result as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(action);
            Assert.AreEqual(action.StatusCode, 400);
            Assert.IsInstanceOfType(result, expectedType: typeof(ActionResult<T>));
        }
    }
}
