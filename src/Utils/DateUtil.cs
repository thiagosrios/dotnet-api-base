using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiBase.Utils
{
    public static class DateUtil
    {
        public static string BasicDateFormat { get; } = "yyyyMMdd";
        public static string BasicHourFormat { get; } = "hhmmss";
        public static string DateTimeFormat { get; } = "yyyyMMdd hh:mm:ss";
        public static string DateTimePtBrFormat { get; } = "dd/MM/yyyy HH:mm:ss";
        public static string CompactDateTimeFormat { get; } = "yyyyMMddhhmmss";
        public static readonly IFormatProvider formatProvider = CultureInfo.CreateSpecificCulture("pt-BR");

        /// <summary>
        /// Converte string para data
        /// </summary>
        /// <param name="dataString">String com informação da data</param>
        /// <param name="format">Formata utilizado na conversão</param>
        /// <returns>DateTime com data formatada</returns>
        public static DateTime ParseStringToDate(string dataString, string format)
        {
            try
            {
                IFormatProvider culture = CultureInfo.InvariantCulture;
                DateTime parse = DateTime.ParseExact(dataString.Trim(), format, culture);

                return Convert.ToDateTime(parse);
            }
            catch (FormatException ex)
            {
                string exceptionMessage = ex.Message.Contains("valid") ? "Valor informado não contém uma data válida" : ""; 
                throw new FormatException("Erro ao formatar data: " + exceptionMessage);
            }
        }

        /// <summary>
        /// Retorno DateTime a partir de string de data, no formato dd/MM/yyyy hh:mm:ss
        /// </summary>
        /// <param name="data">String de data</param>
        /// <returns>Objeto DateTime formatado</returns>
        public static DateTime ParseStringToDateTime(string data)
        {
            try
            {
                DateTime parse = DateTime.ParseExact(data, DateTimePtBrFormat, formatProvider);

                return Convert.ToDateTime(parse);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Erro ao formatar data: " + ex.Message);
            }
        }

        /// <summary>
        /// Conversão de data para string
        /// </summary>
        /// <param name="data">DateTime usado na conversão</param>
        /// <returns>String com data convertida no formato yyyyMMdd hh:mm:ss</returns>
        public static string ParseDateToString(DateTime data)
        {
            return data.ToString(DateTimeFormat);
        }

        /// <summary>
        /// Converte parâmetro inteiro de CData das NFe's para DateTime
        /// </summary>
        /// <param name="cData">Inteiro contendo representação da data</param>
        /// <returns>Objeto DateTime</returns>
        public static DateTime ParseCDataToDateTime(int cData)
        {
            return ParseStringToDate(cData.ToString(), BasicDateFormat);
        }

        /// <summary>
        /// Retorna nome do mês a partir da sua representação numérica
        /// </summary>
        /// <param name="month">Representação numérica do mês</param>
        /// <returns>String contendo nome do mês</returns>
        public static string GetMonthName(int month)
        {
            return CultureInfo.CreateSpecificCulture("pt-br").DateTimeFormat.GetMonthName(month);
        }

        /// <summary>
        /// Generator para permitir loop entre períodos de datas
        /// </summary>
        /// <param name="start">Data inicial</param>
        /// <param name="end">Data final</param>
        /// <returns>Enumerable com intervalos de datas entre meses e anos</returns>
        public static IEnumerable<DateTime> LoopFromDateRange(DateTime start, DateTime end)
        {
            string format = "yyyy-MM";
            string startYearMonth = string.Concat(start.Year.ToString(), "-", start.ToString("MM"));
            string endYearMonth = string.Concat(end.Year.ToString(), "-", end.ToString("MM"));
            DateTime startDate = DateTime.ParseExact(startYearMonth, format, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(endYearMonth, format, CultureInfo.InvariantCulture);

            while (startDate <= endDate)
            {
                yield return startDate; 
                startDate = startDate.AddMonths(1);
            }
        }

        /// <summary>
        /// Converte string de hora em timespan
        /// </summary>
        /// <param name="hour">String de hora no formato hhmmss (sem marcador de divisão)</param>
        /// <returns>Objeto TimeSpan</returns>
        public static TimeSpan ParseStringToTimeSpan(string hour)
        {
            string formattedHour = string.Format("{0}:{1}:{2}", hour.Substring(0, 2), hour.Substring(2, 2), hour.Substring(4, 2));
            if (!TimeSpan.TryParse(formattedHour, out TimeSpan time))
            {
                throw new FormatException("Erro ao converter hora para TimeSpan");
            }

            return time;
        }

        /// <summary>
        /// Retorna inteiro com representação do timestamp
        /// </summary>
        /// <returns>Long com valor do timestamp</returns>
        public static long GetTimestamp()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            return Convert.ToInt64(timestamp);
        }

        /// <summary>
        /// Converte valores de ano e mês para o formato CData (YYYYMM)
        /// </summary>
        /// <param name="year">Ano de referência</param>
        /// <param name="month">Mês de referência</param>
        /// <returns></returns>
        public static int GetCData(int year, int month)
        {
            string monthValue = month < 10 ? month.ToString("D2") : month.ToString("D");

            return Convert.ToInt32("" + year + monthValue);
        }
    }
}