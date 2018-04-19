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
    public partial class ZoneSelectControl : System.Web.UI.UserControl
    {
        public delegate void LoadZoneDelegate();
        public LoadZoneDelegate OnLoadZone;

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
        void BindData()
        {
            try
            {
                var list = ConnectionFactory.Instance.GetZoneList(Page.User.Identity.Name);
                if (list != null)
                {
                    _defaultZone = list[0].Value;
                }
                var zoneCache = SelectControl.GetSelectZoneFromCookie(HttpContext.Current);
                var index = 0;
                if (!string.IsNullOrEmpty(zoneCache) && list!=null)
                {
                    index = list.FindIndex(d => d.Value == zoneCache);
                    _defaultZone = zoneCache;
                }
                

                SZone.DataSource = list;
                SZone.DataTextField = "Text";
                SZone.DataValueField = "Value";
                SZone.DataBind();
                SZone.SelectedIndex = index;
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
                if (SZone.SelectedIndex < 0)
                    return _defaultZone;
                _zoneId = SZone.SelectedItem.Value;
                SelectControl.SetSelectZoneToCookie(HttpContext.Current, _zoneId);
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