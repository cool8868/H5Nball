using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Match;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Match;
using Games.NBall.NB_Web.Helper;
using Games.NBall.UAFacade;
using Games.NBall.UAFacade.UABll;

namespace Games.NBall.NB_Web
{
    public partial class US : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UAFactory.Instance.Adapter.doCheckActive();
        }
    }
}