using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Games.MyControl
{
public class MyControl_FilterHelper
{
    // Methods
    public static void FilterModel(object obj)
    {
        foreach (PropertyInfo info in obj.GetType().GetProperties())
        {
            if ((string.Compare(info.PropertyType.Name, "String", true) == 0) && (info.GetValue(obj, null) != null))
            {
                info.SetValue(obj, HtmlToString(info.GetValue(obj, null).ToString()), null);
            }
        }
    }

    public static int GetAwardLevel(object value)
    {
        int result = 0;
        if ((value != null) && int.TryParse(value.ToString(), out result))
        {
            return result;
        }
        return 0x13;
    }

    public static DateTime GetBeginDateTimeFormat(object value)
    {
        DateTime maxValue = DateTime.MaxValue;
        if ((value != null) && DateTime.TryParse(value.ToString(), out maxValue))
        {
            return Convert.ToDateTime(maxValue.ToString("yyyy-MM-dd 00:00:00"));
        }
        return Convert.ToDateTime("0001-01-01 00:00:00");
    }

    public static bool GetBool(object value)
    {
        bool result = false;
        return (((value != null) && bool.TryParse(value.ToString(), out result)) && result);
    }

    public static byte GetByte(object value)
    {
        byte result = 0x7f;
        if ((value != null) && byte.TryParse(value.ToString(), out result))
        {
            return result;
        }
        return 0x7f;
    }

    public static DateTime GetDateTime(object value)
    {
        DateTime now = DateTime.Now;
        if ((value != null) && DateTime.TryParse(value.ToString(), out now))
        {
            return now;
        }
        return Convert.ToDateTime("0001-01-01 00:00:00");
    }

    public static string GetDateTimeStr(object value)
    {
        DateTime now = DateTime.Now;
        if ((value != null) && DateTime.TryParse(value.ToString(), out now))
        {
            return now.ToString();
        }
        return "";
    }

    public static DateTime GetEndDateTimeFormat(object value)
    {
        DateTime maxValue = DateTime.MaxValue;
        if ((value != null) && DateTime.TryParse(value.ToString(), out maxValue))
        {
            return Convert.ToDateTime(maxValue.ToString("yyyy-MM-dd 23:59:59"));
        }
        return Convert.ToDateTime("0001-01-01 00:00:00");
    }

    public static int GetInt(object value)
    {
        int result = 0;
        if ((value != null) && int.TryParse(value.ToString(), out result))
        {
            return result;
        }
        return -1;
    }

    public static int GetInt(object value, int returnValue)
    {
        int result = returnValue;
        if ((value != null) && int.TryParse(value.ToString(), out result))
        {
            return result;
        }
        return returnValue;
    }

    public static int GetIntOne(object value)
    {
        int result = 0;
        if ((value != null) && int.TryParse(value.ToString(), out result))
        {
            return result;
        }
        return 1;
    }

    public static int GetIntZero(object value)
    {
        int result = 0;
        if ((value != null) && int.TryParse(value.ToString(), out result))
        {
            return result;
        }
        return 0;
    }

    public static List<T> GetList<T>(DataTable dt)
    {
        List<T> list = new List<T>();
        if ((dt != null) && (dt.Rows.Count > 0))
        {
            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetModel<T>(row));
            }
        }
        return list;
    }

    public static DateTime GetMaxDate(object value)
    {
        DateTime maxValue = DateTime.MaxValue;
        if ((value != null) && DateTime.TryParse(value.ToString(), out maxValue))
        {
            return maxValue;
        }
        return DateTime.MaxValue;
    }

    public static DateTime GetMinDate(object value)
    {
        DateTime minValue = DateTime.MinValue;
        if ((value != null) && DateTime.TryParse(value.ToString(), out minValue))
        {
            return minValue;
        }
        return DateTime.MinValue;
    }

    public static T GetModel<T>(DataRow dr)
    {
        Type type = typeof(T);
        T local = (T) type.Assembly.CreateInstance(type.FullName);
        foreach (PropertyInfo info in type.GetProperties())
        {
            if (dr.Table.Columns.Contains(info.Name))
            {
                info.SetValue(local, GetPropType(info.PropertyType.Name, dr[info.Name].ToString()), null);
            }
        }
        return local;
    }

    public static T GetModel<T>(SqlDataReader dr)
    {
        Type type = typeof(T);
        T local = (T) type.Assembly.CreateInstance(type.FullName);
        foreach (PropertyInfo info in type.GetProperties())
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Contains(info.Name))
                {
                    info.SetValue(local, GetPropType(info.PropertyType.Name, dr[info.Name].ToString()), null);
                    break;
                }
            }
        }
        return local;
    }

    public static object GetPropType(string type, string value)
    {
        object obj2 = new object();
        switch (type)
        {
            case "Int32":
                return Convert.ToInt32(value);

            case "UInt32":
                return Convert.ToUInt32(value);

            case "Int16":
                return Convert.ToInt16(value);

            case "UInt16":
                return Convert.ToUInt16(value);

            case "Int64":
                return Convert.ToInt64(value);

            case "UInt64":
                return Convert.ToUInt64(value);

            case "String":
                return GetString(value);

            case "Single":
                return Convert.ToSingle(value);

            case "Double":
                return Convert.ToDouble(value);

            case "Decimal":
                return Convert.ToDecimal(value);

            case "Char":
                return Convert.ToChar(value);

            case "Boolean":
                return Convert.ToBoolean(value);

            case "Byte":
                return Convert.ToByte(value);

            case "SByte":
                return Convert.ToSByte(value);

            case "DateTime":
                return GetDateTime(value);
        }
        return GetString(value);
    }

    public static string GetSltStr(object value)
    {
        return GetInt(value).ToString();
    }

    public static string GetString(object value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        return value.ToString().Trim();
    }

    public static string HtmlToString(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            string oldValue = @"\";
            str = str.Replace(oldValue, oldValue + oldValue);
            str = str.Replace("'", "");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
        }
        return str;
    }

    public static string IpAddress()
    {
        bool flag;
        return IpAddress(out flag);
    }

    private static string IpAddress(out bool isProxy)
    {
        HttpContext current = HttpContext.Current;
        if (current == null)
        {
            isProxy = false;
            return "Unknow";
        }
        string str = current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        isProxy = current.Request.ServerVariables["HTTP_VIA"] != null;
        if (!string.IsNullOrEmpty(str))
        {
            if (!isProxy)
            {
                isProxy = str.IndexOf(',') > -1;
            }
            if (str.IndexOf('.') > 0)
            {
                return str;
            }
        }
        return (current.Request.UserHostAddress ?? "Unknow");
    }
}
}
