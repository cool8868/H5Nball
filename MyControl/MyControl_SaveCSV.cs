using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Games.MyControl
{
internal class MyControl_SaveCSV
{
    // Fields
    public HttpResponse response;

    // Methods
    public MyControl_SaveCSV(HttpResponse response)
    {
        this.response = response;
    }

    public static string ExcelNewLine(string oldString)
    {
        string str = oldString;
        str = str.Replace("\r\n", ";").Replace(';', '\n');
        return ('"' + str + '"');
    }

    public static string ExcelNewLine(string oldString, string oldChar)
    {
        string str = oldString;
        str = str.Replace(oldChar, ";").Replace(';', '\n');
        return ('"' + str + '"');
    }

    public bool OutFile(string fullPath, string content)
    {
        bool flag = false;
        try
        {
            this.response.Clear();
            this.response.Buffer = true;
            this.response.Charset = Encoding.Default.BodyName;
            this.response.ContentEncoding = Encoding.GetEncoding("GB2312");
            this.response.AppendHeader("content-Disposition", "attachment;filename=" + fullPath);
            this.response.ContentType = "application/ms-excel";
            this.response.Output.Write(content);
            this.response.Flush();
            flag = true;
        }
        catch (Exception exception)
        {
            MyControl_MessageBox.Msg(string.Format("输出数据CSV出错:{0}", exception.Message));
            flag = false;
        }
        finally
        {
            HttpContext.Current.Response.Close();
        }
        return flag;
    }
}
}
