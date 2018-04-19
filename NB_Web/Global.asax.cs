using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.NB_Web
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {
            
            //Common.RandomHelper.Initialize();
        }
       
        void SetEveryDay(object sender, System.Timers.ElapsedEventArgs e)
        {
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception objErr = Server.GetLastError().GetBaseException();
                Server.ClearError();
                SystemlogMgr.Error("Application_Error", objErr);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}