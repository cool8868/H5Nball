using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Games.MyControl
{
public class MyControl_MessageBox
{
    // Methods
    public static void Msg(string strMsg)
    {
        HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + strMsg + "');</script>");
    }

    public static void Msg(Page page, string strMsg)
    {
        page.ClientScript.RegisterStartupScript(typeof(string), "msg", "<script type=\"text/javascript\">alert('" + strMsg + "')</script>");
    }

    public static void Msg(Page page, string strMsg, string strUrl)
    {
        page.ClientScript.RegisterStartupScript(typeof(string), "msg", "<script type=\"text/javascript\">alert('" + strMsg + "');top.location.href='" + strUrl + "';</script>");
    }

    public static void MsgBack(Page page, string strMsg)
    {
        page.ClientScript.RegisterStartupScript(typeof(string), "msg", "<script type=\"text/javascript\">alert('" + strMsg + "');window.location.href='javascript:history.back()';</script>");
    }

    public static void MsgDo(Page page, string strMsg, string strDo)
    {
        page.ClientScript.RegisterStartupScript(typeof(string), "msg", "<script type=\"text/javascript\">alert('" + strMsg + "');" + strDo + "</script>");
    }

    public static void MsgOpen(Page page, string strMsg, string strUrl)
    {
        page.ClientScript.RegisterStartupScript(typeof(string), "msg", "<script type=\"text/javascript\">alert('" + strMsg + "');window.open('" + strUrl + "');</script>");
    }

    public static void Redirect(string strUrl)
    {
        HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.location.href='" + strUrl + "';</script>");
    }

    public static void Redirect(string strMsg, string strUrl)
    {
        HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + strMsg + "');window.location.href='" + strUrl + "';</script>");
    }

    public static void WindowClose(Page page)
    {
        page.ClientScript.RegisterStartupScript(typeof(string), "msg", "<script type=\"text/javascript\">window.close();</script>");
    }
}
}
