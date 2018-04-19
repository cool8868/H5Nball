using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.UserControls
{
    public partial class ZoneSelectControl2 : System.Web.UI.UserControl
    {
        public delegate void LoadZoneDelegate();
        public ZoneSelectControl.LoadZoneDelegate OnLoadZone;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
            if (OnLoadZone != null)
                OnLoadZone();
        }

        private string _defaultZone;
        public void BindData()
        {
            try
            {
                var list = ConnectionFactory.Instance.GetZoneList(Page.User.Identity.Name);
                if (list != null)
                {
                    _defaultZone = list[0].Value;
                }
                this.SZone.DataSource = list;
                this.SZone.DataTextField = "Text";
                this.SZone.DataValueField = "Value";
                this.SZone.DataBind();
                if (!string.IsNullOrEmpty(_zoneId))
                    this.SZone.SelectedValue = _zoneId;
                else
                {
                    this.SZone.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }

        private string _zoneId;
        public string ZoneId
        {
            get
            {
                if (this.SZone.SelectedIndex < 0)
                    return _defaultZone;
                _zoneId = this.SZone.SelectedItem.Value;
                return _zoneId;
            }
            set
            {
                SZone.SelectedValue = value;
            }
        }

        public int ZoneIdInt
        {
            get { return ConnectionFactory.Instance.GetZoneId(ZoneId); }
        }
    }
}