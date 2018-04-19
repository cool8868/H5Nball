using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.UAFacade;

namespace Games.NBall.NB_Web
{
    public partial class Uc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UAFactory.Instance.Adapter.doCharge();
        }
    }
}