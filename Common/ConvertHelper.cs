using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Common
{
    public class ConvertHelper
    {
        /// <summary>
        /// 如果转换失败，返回[defaultValue].
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ConvertToInt(string param, int defaultValue)
        {
            if (string.IsNullOrEmpty(param))
                return defaultValue;
            int x = defaultValue;
            bool isSuccess = int.TryParse(param, out x);
            if (isSuccess)
                return x;
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 如果转换失败，返回[defaultValue].
        /// </summary>
        /// <param name="param"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ConvertToDouble(string param, double defaultValue)
        {
            if (string.IsNullOrEmpty(param))
                return defaultValue;
            double x = defaultValue;
            bool isSuccess = double.TryParse(param, out x);
            if (isSuccess)
                return x;
            else
            {
                return defaultValue;
            }
        }

        public static long ConvertToLong(string param, long defaultValue)
        {
            if (string.IsNullOrEmpty(param))
                return defaultValue;
            long x = defaultValue;
            bool isSuccess = long.TryParse(param, out x);
            if (isSuccess)
                return x;
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 如果转换失败，返回[defaultValue].
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(string param, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(param))
                return defaultValue;
            decimal x = defaultValue;
            bool isSuccess = decimal.TryParse(param, out x);
            if (isSuccess)
                return x;
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 把bool值转换为int，为true=1否则=0
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ConvertToInt(bool param)
        {
            return param ? 1 : 0;
        }

        /// <summary>
        /// 为1表示true，其他false
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool ConvertToBool(string param)
        {
            if (string.IsNullOrEmpty(param))
                return false;
            else if (param == "1")
                return true;
            else
                return false;

        }

        public static int ConvertToInt(decimal param)
        {
            return Convert.ToInt32(param);
        }

        /// <summary>
        /// 如果转换失败，默认返回0
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static int ConvertToInt(string param)
        {
            return ConvertToInt(param, 0);
        }
        /// <summary>
        /// 如果转换失败，默认返回0
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static double ConvertToDouble(string param)
        {
            return ConvertToDouble(param, 0);
        }
        /// <summary>
        /// 枚举转换成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static string ConvertEnumToString<T>(T enumObj)
        {
            return Enum.GetName(typeof(T), enumObj);
        }
        /// <summary>
        /// 字符串转换成时间（默认返回当前时间）
        /// </summary>
        /// <returns></returns>
        public static DateTime StringToDateTime(string str)
        {
            try
            {
                var date = Convert.ToDateTime(str);
                return date;
            }
            catch (Exception)
            {

            }

            return DateTime.Now;
        }
    }
}
