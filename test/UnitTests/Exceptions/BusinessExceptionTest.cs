using ApiBase.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApiBaseTest.UnitTests.Exceptions
{
    [TestClass]
    public class BusinessExceptionTest
    {
        [TestMethod]
        public void TestBusinessException()
        {
            string message = "Ocorreu um erro";
            Exception ex = new BusinessException(message);
            string exceptionToString = ex.ToString();

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                // Salva estado do objeto
                bf.Serialize(ms, ex);

                // Reutiliza o mesmo stream para desserialização
                ms.Seek(0, 0);

                // Substitui a exceção original com a desserializada
                ex = (BusinessException)bf.Deserialize(ms);
            }

            Assert.IsNotNull(ex);
            Assert.IsNotNull(exceptionToString);
            Assert.AreEqual(exceptionToString, ex.ToString(), "ex.ToString()");
            Assert.AreEqual(ex.Message, message);
        }
    }
}
