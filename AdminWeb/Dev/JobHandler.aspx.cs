using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Entity.Enums;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.AdminWeb.Dev
{
    public partial class JobHandler : System.Web.UI.Page
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDailycupCreate_Click(object sender, EventArgs e)
        {
            var code = dailycupClient.JobCreate();
            if (code != MessageCode.Success)
            {
                
            }
        }
        #endregion

        #region encapsulation
        private readonly DailycupClient dailycupClient = new DailycupClient();
        #endregion
    }
}