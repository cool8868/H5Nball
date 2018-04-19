using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Gamble;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.AdminWeb.GmTools
{
    public partial class GiveReward : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    BindZone();
                }
            }

            void BindZone()
            {
                SZone.DataSource = ConnectionFactory.Instance.GetZoneList(this.User.Identity.Name);
                SZone.DataTextField = "Text";
                SZone.DataValueField = "Value";
                SZone.DataBind();
                if (!string.IsNullOrEmpty(_zoneId))
                    SZone.SelectedValue = _zoneId;
                else
                {
                    SZone.SelectedIndex = 0;
                }
            }

            private string _zoneId;
            public string ZoneId
            {
                get
                {
                    _zoneId = SZone.SelectedItem.Value;
                    return _zoneId;
                }
                set
                {
                    SZone.SelectedValue = value;
                }
            }
            void ShowMessage(string msg)
            {
                ltlMessage.Text = msg;
            }

            protected void btnSend_Click(object sender, EventArgs e)
            {
                if (GambleCore.Instance.GiveRankReward(ZoneId))
                {
                    ShowMessage("奖励发放成功!");
                }
                else
                {
                    ShowMessage("奖励发放失败!");
                }
            }
        }
    }
