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
using Games.NBall.Core.League;
using Games.NBall.Entity.Response.Auction;

namespace Games.NBall.AdminWeb.GmTools
{
    public partial class AddGambleHost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindZone();
                BindTitles();
                BindOptions();
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

        void BindTitles()
        {
            try
            {
                if (SZone.Items.Count == 0)
                {
                    ShowMessage("未获取到区服信息，请联系开发人员！");
                    return;
                }
                List<GambleTitleEntity> listTitles = GambleTitleMgr.GetCanHostStartList(ZoneId);
                if (listTitles == null || listTitles.Count == 0)
                {
                    ShowMessage("当前没有可以坐庄的主题，请先发布竞猜主题");
                    return;
                }
                SZTitles.DataSource = listTitles;
                SZTitles.DataTextField = "Title";
                SZTitles.DataValueField = "Idx";
                SZTitles.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        //void BindOptions()
        //{
        //    cblOptions.DataSource = null;
        //    if(SZTitles.Items.Count == 0)
        //        return;
        //    List<GambleOptionEntity> optionList = GambleOptionMgr.GetByTitleId(new Guid(SZTitles.SelectedValue),ZoneId);
        //    cblOptions.DataSource = optionList;
        //    cblOptions.DataTextField = "OptionContent";
        //    cblOptions.DataValueField = "Idx";
        //    cblOptions.DataBind();
        //}

        void BindOptions()
        {
            blOptions.DataSource = null;
            if (SZTitles.Items.Count == 0)
                return;
            List<GambleOptionEntity> optionList = GambleOptionMgr.GetByTitleId(new Guid(SZTitles.SelectedValue), ZoneId);
            blOptions.DataSource = optionList;
            blOptions.DataTextField = "OptionContent";
            blOptions.DataValueField = "Idx";
            blOptions.DataBind();
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
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            for (int i = 0, count = SZone.Items.Count; i < count; i++)
            {
                try
                {
                    AuctionBuyResponse mc = GambleCore.Instance.ToBeHost(LeagueConst.GambleNpcId, txtRates.Text, new Guid(SZTitles.SelectedValue), Convert.ToInt32(txtMoney.Text), SZone.Items[i].Value);
                    if (mc.Code != (int)MessageCode.Success)
                        msg.Append(SZone.Items[i].Text + ":" + mc.Code + "|");
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                }
            }
            if (msg.Length > 0)
            {
                ShowMessage(msg.ToString() + "坐庄失败！");
            }
            else
            {
                ShowMessage("坐庄成功！");
            }

        }

        protected void SZTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOptions();
        }


    }
}