﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Common;
using Games.NBall.NB_Web.Helper;
using Games.NBall.UAFacade;

namespace Games.NBall.NB_Web
{
    public partial class Ut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           UAFactory.Instance.Adapter.doOtherOne();
           
        }
    }
}