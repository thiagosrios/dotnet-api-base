using ApiBase.Utils;
using ApiBaseTest.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiBaseTest.UnitTests.Utils
{
    [TestClass]
    public class DateUtilTest
    {
        [TestMethod]
        public void TestConverterStringDataEmDate()
        {
            DateTime date = DateUtil.ParseStringToDate("20190501", DateUtil.BasicDateFormat.ToString());

            Assert.That.IsNotNullAndIsDatetime(date);
        }

        [TestMethod]
        public void TestConverterStringDataEmHour()
        {
            DateTime date = DateUtil.ParseStringToDate("113000", DateUtil.BasicHourFormat.ToString());

            Assert.That.IsNotNullAndIsDatetime(date); 
        }

        [TestMethod]
        public void TestConverterStringCompactDateTime()
        {
            DateTime date = DateUtil.ParseStringToDate("20200101000000", DateUtil.CompactDateTimeFormat.ToString());

            Assert.That.IsNotNullAndIsDatetime(date); 
        }

        [TestMethod]
        public void TestConvertStringToDateExcpetion()
        {
            Assert.ThrowsException<FormatException>(() =>
                DateUtil.ParseStringToDate("01/01/2019 10:00:00", DateUtil.DateTimeFormat)
            );
        }

        [TestMethod]
        public void TestGeraExcecaoParaArgumentoInvalido()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
                DateUtil.ParseStringToDate(null, DateUtil.BasicDateFormat)
            );
        }

        [TestMethod]
        public void TestGeraExcecaoParaDataInvalida()
        {
            Assert.ThrowsException<FormatException>(() =>
                DateUtil.ParseStringToDate("2019/06/01", DateUtil.BasicDateFormat)            
            );
        }

        [TestMethod]
        public void TestGeraExcecaoParaFormatoInvalido()
        {
            Assert.ThrowsException<FormatException>(() =>
                DateUtil.ParseStringToDate("20190601", "abcdef")
            );
        }

        [TestMethod]
        public void TestConverterDateTimeAtualEmString()
        {
            string date = DateUtil.ParseDateToString(DateTime.Now);

            Assert.That.IsNotNullAndIsString(date);
        }

        [TestMethod]
        public void TestConvertStringEmDateTime()
        {
            DateTime data = DateUtil.ParseStringToDateTime("01/01/2019 10:00:00");

            Assert.IsNotNull(data);
            Assert.AreEqual(data.Day, 01);
            Assert.AreEqual(data.Month, 01);
            Assert.AreEqual(data.Year, 2019);
        }

        [TestMethod]
        public void TestConvertStringEmDateTimeExcpetion()
        {
            Assert.ThrowsException<FormatException>(() =>
                DateUtil.ParseStringToDateTime("20190101 10:00:00")
            );
        }

        [TestMethod]
        public void TestConverterCDataToDateTime()
        {
            DateTime date = DateUtil.ParseCDataToDateTime(20190101);

            Assert.That.IsNotNullAndIsDatetime(date);
        }

        [TestMethod]
        public void TestConvertCDataToDateTimeExcpetion()
        {
            Assert.ThrowsException<FormatException>(() =>
                DateUtil.ParseCDataToDateTime(20193112)
            );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void TestGetMonthName(int month)
        {
            string mes = DateUtil.GetMonthName(month);

            Assert.That.IsNotNullAndIsString(mes);
            Assert.IsFalse(string.IsNullOrEmpty(mes));
        }

        [TestMethod]
        public void TestLoopFromDateRange()
        {
            DateTime date = DateTime.Now;
            IEnumerable<DateTime> result = DateUtil.LoopFromDateRange(date, date.AddMonths(10));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.Count(), 11);
        }

        [TestMethod]
        public void TestParseStringToTimeSpan()
        {
            TimeSpan result = DateUtil.ParseStringToTimeSpan("122556");

            Assert.That.IsNotNullAndIsDatetime(result);
        }

        [TestMethod]
        public void TestParseStringToTimeSpanException()
        {
            FormatException ex = Assert.ThrowsException<FormatException>(() =>
                DateUtil.ParseStringToTimeSpan("12-25-56")
            );

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex.GetType(), typeof(FormatException).GetType());
            Assert.AreEqual(ex.Message, "Erro ao converter hora para TimeSpan");
        }

        [TestMethod]
        public void TestGetTimestamp()
        {
            long result = DateUtil.GetTimestamp();

            Assert.That.IsNotNullAndIsDecimalWithValue(result);
        }

        [TestMethod]
        [DataRow(2019, 1)]
        [DataRow(2019, 10)]
        public void TestGetCDate(int ano, int mes)
        {
            int CData = DateUtil.GetCData(ano, mes);

            Assert.That.IsNotNullAndIsInt(CData);
        }
    }
}
