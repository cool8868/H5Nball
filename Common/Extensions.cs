using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Collections;

namespace Games
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extension
    {
        #region ConcurrentDictionary
        /// <summary>
        /// 尝试从 System.Collections.Concurrent.ConcurrentDictionary(TKey,TValue) 中移除并返回具有指定键的值。
        /// </summary>
        /// <typeparam name="TKey">TKey</typeparam>
        /// <typeparam name="TValue">TValue</typeparam>
        /// <param name="dict">要处理的集合</param>
        /// <param name="key">要移除并返回的元素的键。</param>
        /// <param name="defaultValue">此方法返回时，value 包含从 System.Collections.Concurrent.ConcurrentDictionary(TKey,TValue)
        /// 中移除的对象；如果操作失败，则包含默认值。
        /// </param>
        /// <returns>如果成功移除了对象，则为 true；否则为 false。</returns>
        public static bool TryRemoveWith<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            return dict.TryRemove(key, out value);
        }

        /// <summary>
        /// 尝试从 System.Collections.Concurrent.ConcurrentDictionary(TKey,TValue) 获取与指定的键关联的值。
        /// </summary>
        /// <typeparam name="TKey">TKey</typeparam>
        /// <typeparam name="TValue">TValue</typeparam>
        /// <param name="dict">要处理的集合</param>
        /// <param name="key">要获取的值的键。</param>
        /// <param name="defaultValue">此方法返回时，value 包含 System.Collections.Concurrent.ConcurrentDictionary(TKey,TValue)
        /// 中具有指定键的对象；如果操作失败，则包含默认值。
        /// </param>
        /// <returns>如果在 System.Collections.Concurrent.ConcurrentDictionary(TKey,TValue) 中找到该键，则为TValue，否则为NULL</returns>
        public static TValue TryGetValueWith<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue TryGetValueWith<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }
        #endregion

        #region string.Format
        /// <summary>
        ///  将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        /// <summary>
        ///  将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }
        /// <summary>
        ///  将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象</param>
        /// <param name="arg1">要设置格式的第二个对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }
        /// <summary>
        ///  将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象</param>
        /// <param name="arg1">要设置格式的第二个对象</param>
        /// <param name="arg2">要设置格式的第三个对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        #endregion

        #region string.Join
        /// <summary>
        /// 串联类型为 System.String 的 System.Collections.Generic.IEnumerable<T> 构造集合的成员，其中在每个成员之间使用指定的分隔符。
        /// </summary>
        /// <param name="values">一个包含要串联的字符串的集合。</param>
        /// <param name="separator">要用作分隔符的字符串。</param>
        /// <returns>一个由 values 的成员组成的字符串，这些成员以 separator 字符串分隔。</returns>
        public static string Join(this object values, string separator)
        {
            if (values.GetType() == typeof(System.Object[]))
            {
                object[] v = (object[])values;
                return string.Join(separator, v);
            }
            else if (values.GetType() == typeof(System.String[]))
            {
                string[] v = (string[])values;
                return string.Join(separator, v);
            }
            else if (values.GetType() == typeof(System.Collections.Generic.List<string>))
            {
                List<string> v = (List<string>)values;
                return string.Join(separator, v);
            }
            return string.Join(separator, values);
        }

        #endregion

        #region string.IsNullOrWhiteSpace
        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="s">要验证的字符串。</param>
        /// <returns>如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。</returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        #endregion

        #region string.Concat
        /// <summary>
        /// 连接指定 System.Object 数组中的元素的 System.String 表示形式，包含当前的字符串。
        /// </summary>
        /// <param name="s">返回</param>
        /// <param name="args">一个对象数组，其中包含要连接的元素。</param>
        /// <returns>args 中元素的值经过连接的字符串表示形式。</returns>
        public static string ConcatWith(this string s, params object[] args)
        {
            foreach (var item in args)
            {
                s += item;
            }
            return s;
        }
        #endregion

        #region 数据类型转换
        /// <summary>
        /// 将一个对象转换成Int32
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回的值，默认为default(int)</param>
        /// <returns></returns>
        public static int ToInt32(this object obj, int defaultValue = default(int))
        {
            int value;
            if (int.TryParse(obj.ToString(), out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将一个字符串转换成Int32
        /// </summary>
        /// <param name="s">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回的值，默认为default(int)</param>
        /// <returns></returns>
        public static int ToInt32(this string s, int defaultValue = default(int))
        {
            int value;
            if (int.TryParse(s, out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将一个对象转换成Int64
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回的值，默认为default(long)</param>
        /// <returns></returns>
        public static long ToInt64(this object obj, long defaultValue = default(long))
        {
            long value;
            if (long.TryParse(obj.ToString(), out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将一个字符串转换成Int64
        /// </summary>
        /// <param name="s">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回的值，默认为default(long)</param>
        /// <returns></returns>
        public static long ToInt64(this string s, long defaultValue = default(long))
        {
            long value;
            if (long.TryParse(s, out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将一个对象转换成DateTime
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回的值，默认为default(DateTime)</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, DateTime defaultValue = default(DateTime))
        {
            DateTime value;
            if (DateTime.TryParse(obj.ToString(), out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将一个字符串转换成DateTime
        /// </summary>
        /// <param name="s">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回的值，默认为default(DateTime)</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, DateTime defaultValue = default(DateTime))
        {
            DateTime value;
            if (DateTime.TryParse(s, out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将一个对象转换成字符串
        /// </summary>
        /// <param name="obj">如果对象为NULL，则返回string.Empty</param>
        /// <returns></returns>
        public static string ToStringEmpty(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 将一个字符串转换成字符串
        /// </summary>
        /// <param name="s">如果对象为NULL，则返回string.Empty</param>
        /// <returns></returns>
        public static string ToStringEmpty(this string s)
        {
            if (s == null)
            {
                return string.Empty;
            }

            return s;
        }

        #endregion

        #region 获取两个时间秒差 返回大于等于0的秒数
        /// <summary>
        /// 获取两个时间秒差
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <param name="cdTime">过期时间</param>
        /// <returns>返回大于等于0的秒数</returns>
        public static long GetSpanTime(this DateTime nowTime, DateTime cdTime)
        {
            int seconds = (int)Math.Ceiling((cdTime - nowTime).TotalSeconds);
            return seconds > 0 ? seconds : 0;
        }
        #endregion

        #region 随机数
        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="minValue">返回的随机数的下界(随机数可取该下界值)</param>
        /// <param name="maxValue">返回的随机数的上界(随机数不能取该上界值)。maxValue必须大于或等于minValue</param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(minValue, maxValue);
        }

        /// <summary>
        /// 返回一个小于所指定最大值的非负随机数
        /// </summary>
        /// <param name="maxValue">要生成的随机数的上限(随机数不能取该上限值)。maxValue必须大于或等于零</param>
        /// <returns></returns>
        public static int Next(int maxValue)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(maxValue);
        }
        #endregion
    }
}
