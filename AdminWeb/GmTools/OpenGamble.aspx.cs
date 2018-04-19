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
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;


namespace Games.NBall.AdminWeb.GmTools
{
    public partial class OpenGamble : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
                BindTitles();
            }
        }

        void BindTitles()
        {
            List<GambleTitleEntity> list = GambleTitleMgr.GetNeedOpenGambleTitles(ZoneId);
            ddltNeedOpenGambleTitle.DataSource = list;
            ddltNeedOpenGambleTitle.DataTextField = "Title";
            ddltNeedOpenGambleTitle.DataValueField = "Idx";
            ddltNeedOpenGambleTitle.DataBind();
            if (list.Count > 0)
                BindOptions();
            else
            {
                rblOptions.DataSource = null;
            }
        }

        void BindOptions()
        {
            if (ddltNeedOpenGambleTitle.Items.Count == 0)
                return;
            GambleTitleEntity title = GambleTitleMgr.GetById(new Guid(ddltNeedOpenGambleTitle.SelectedValue), ZoneId);

            List<GambleOptionEntity> list = GambleOptionMgr.GetByTitleId(new Guid(ddltNeedOpenGambleTitle.SelectedValue), ZoneId);
            rblOptions.DataSource = list;
            rblOptions.DataTextField = "OptionContent";
            rblOptions.DataValueField = "Idx";
            rblOptions.DataBind();

            if (title.ResultFlagId != Guid.Empty)
            {
                for (int i = 0, count = rblOptions.Items.Count; i < count; i++)
                {
                    if (new Guid(rblOptions.Items[i].Value) == title.ResultFlagId)
                    {
                        rblOptions.Items[i].Selected = true;
                        return;
                    }
                }
            }
        }

        void BindControl()
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
            try
            {
                if (ddltNeedOpenGambleTitle.Items.Count == 0)
                    return;

                if (rblOptions.SelectedIndex < 0)
                {
                    ShowMessage("您需要选择一个正确选项");
                    return;
                }
                System.Text.StringBuilder msg = new System.Text.StringBuilder();
                if (chkAll.Checked)
                {
                    for (int i = 0, count = SZone.Items.Count; i < count; i++)
                    {
                        try
                        {
                            GambleTitleEntity title = GambleTitleMgr.GetById(new Guid(ddltNeedOpenGambleTitle.SelectedValue), SZone.Items[i].Value);
                            if (title == null)
                            {
                                msg.Append(SZone.Items[i].Text + "|");
                                continue;
                            }

                            title.ResultFlagId = new Guid(rblOptions.SelectedValue);
                            if (!GambleTitleMgr.Update(title, null, SZone.Items[i].Value))
                                msg.Append(SZone.Items[i].Text + "|");
                        }
                        catch (Exception ex)
                        {
                            msg.Append(SZone.Items[i].Text + ";错误信息" + ex.Message + "|");
                        }
                    }
                }
                else
                {
                    GambleTitleEntity title = GambleTitleMgr.GetById(new Guid(ddltNeedOpenGambleTitle.SelectedValue), ZoneId);
                    if (title == null)
                    {
                        ShowMessage("竞猜主题为空，不能公布任何选项！");
                        return;
                    }
                    //System.Text.StringBuilder msg = new System.Text.StringBuilder();
                    title.ResultFlagId = new Guid(rblOptions.SelectedValue);
                    if (!GambleTitleMgr.Update(title, null, ZoneId))
                    {
                        msg.Append(SZone.SelectedItem.Text + "|");
                    }
                }
                if (msg.Length > 0)
                {
                    ShowMessage(msg.ToString() + "发布正确选项失败！");
                }
                else
                {
                    ShowMessage("成功发布正确选项！");
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        protected void SZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTitles();
        }

        protected void ddltNeedOpenGambleTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOptions();
        }
    }
}