using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.AdminWeb.Tools;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Mail;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.AdminWeb.GmTools
{
    public partial class SendMailCross : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
            }
        }

        void BindControl()
        {
            List<StatusList> list = new List<StatusList>();
            list.Add(new StatusList("1","游戏币"));
            list.Add(new StatusList("2", "点券"));
            list.Add(new StatusList("3", "物品"));
            list.Add(new StatusList("4", "声望"));
            list.Add(new StatusList("8", "绑劵"));
            list.Add(new StatusList("9", "阅历"));
            ddlType.DataTextField = "Text";
            ddlType.DataValueField = "Value";
            ddlType.DataSource = list;
            ddlType.DataBind();
            ddlType.SelectedIndex = 0;
            datagrid1.DataSource = null;
            datagrid1.DataBind();
            LocalAttachment = null;
        }

        
        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            var attachments = GetAttachments();
            var type = ConvertHelper.ConvertToInt(ddlType.SelectedValue);
            var typeStr = ddlType.SelectedItem.Text;
            var count = ConvertHelper.ConvertToInt(txtCount.Text);
            if (count <= 0)
            {
                ShowMessage("数量必须大于0");
                return;
            }
            var itemCode = 0;
            var strength = 0;
            var isBinding = false;
            var name = typeStr;
            if (type == 3)
            {
                isBinding = chkBinding.Checked;
                itemCode = ConvertHelper.ConvertToInt(txtItemCode.Text);
                var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
                if (item == null)
                {
                    ShowMessage("不存在这样的物品，code：" + itemCode);
                    return;
                }
                name = item.ItemName;
                strength = ConvertHelper.ConvertToInt(txtStrength.Text);
                if (item.ItemType == (int)EnumItemType.PlayerCard)
                {
                    if (strength < 1 || strength > 9)
                    {
                        ShowMessage("球员卡强化级别错误，输入：" + strength);
                        return;
                    }
                }
                else if (item.ItemType == (int)EnumItemType.Equipment)
                {
                    if (strength < 0 || strength > 15)
                    {
                        ShowMessage("装备强化级别错误，输入：" + strength);
                        return;
                    }
                }
                else
                {
                    strength = 0;
                }
            }
            attachments.Add(new LocalMailAttachmentEntity(type,typeStr,count,name,itemCode,strength,isBinding));
            datagrid1.DataSource = attachments;
            datagrid1.DataBind();
            LocalAttachment = attachments;
        }

        List<LocalMailAttachmentEntity> GetAttachments()
        {
            return LocalAttachment;
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                string notSendString = "";
                string sendString = txt_SendString.Text;
                string mailName = txt_MailName.Text;
                string info = txt_mailInfo.Text;
                if (sendString.Trim().Length == 0|| mailName.Trim().Length<= 0 ||info.Trim().Length<= 0)
                {
                    ShowMessage("请输入串");
                    return;
                }
                var malls = sendString.Split('|');
                List<LocalMailManagerCorssEntity> managers = new List<LocalMailManagerCorssEntity>();
                foreach (var s in malls)
                {
                    var sss = s.Split(',');
                    var manager = NbManagerMgr.GetByAccount(sss[1], sss[0]);
                    if (manager == null || manager.Count < 1)
                    {
                        ShowMessage("未找到对应经理，账号:" + s);
                        return;
                    }
                    managers.Add(new LocalMailManagerCorssEntity() { ManagerId = manager[0].Idx, ZoneId = sss[0], Account = sss[1],Point = ConvertHelper.ConvertToInt(sss[2]) });
                }
                MailBuilder mail = null;
                foreach (var item in managers)
                {
                    try
                    {
                        if (item.Point <= 0)
                            continue;
                        mail = new MailBuilder(mailName, info);
                        //mail = new MailBuilder(item.ManagerId, mailName);
                        mail.AddAttachment(EnumCurrencyType.Point, item.Point); 
                        var mailInfo = mail.MailInfo;
                        var mailEntity = mailInfo.Clone();
                        mailEntity.ManagerId = item.ManagerId;
                        if (!MailInfoMgr.Insert(mailEntity, null, item.ZoneId))
                        {
                            notSendString += item.ZoneId + "," + item.Account + "," + item.Point + "|";
                            LogHelper.Insert(item.ZoneId + "," + item.Account + "," + item.Point + "-----失败",
                                LogType.Info);
                        }
                        else
                            LogHelper.Insert(item.ZoneId + "," + item.Account + "," + item.Point + "-----成功",
                                LogType.Info);
                    }
                    catch (Exception ex)
                    {
                        notSendString += item.ZoneId+ "," +item.Account + "," +item.Point + "|";
                        LogHelper.Insert(item.ZoneId + "," + item.Account + "," + item.Point + "-----失败",
                                LogType.Info);
                    }
                }
                if (notSendString.Trim().Length > 0)
                    ShowMessage("未成功发送的串:" + notSendString);
                else
                    ShowMessage("发送成功");
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage("发送失败：" + ex.Message);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var users = txtUserList.Text;
                if (string.IsNullOrEmpty(users))
                {
                    ShowMessage("请选择用户");
                    return;
                }
                var title = txtMailTitle.Text;
                if (string.IsNullOrEmpty(title))
                {
                    ShowMessage("请输入标题");
                    return;
                }
                var content = txtMailContent.Text;
                if (string.IsNullOrEmpty(content))
                {
                    ShowMessage("请输入内容");
                    return;
                }
                List<LocalMailManagerCorssEntity> managers = new List<LocalMailManagerCorssEntity>();
                var ss = users.Split('|');
                foreach (var s in ss)
                {
                    var sss = s.Split(',');
                    var manager =NbManagerMgr.GetByAccount(sss[1],sss[0]);
                    if (manager == null || manager.Count<1)
                    {
                        ShowMessage("未找到对应经理，账号:"+s);
                        return;
                    }
                    managers.Add(new LocalMailManagerCorssEntity(){ManagerId = manager[0].Idx,ZoneId = sss[0],Account = sss[1]});
                }

                MailBuilder mail = new MailBuilder(title,content);
                var attachments = GetAttachments();
                if (attachments.Count > 0)
                {
                    foreach (var entity in attachments)
                    {
                        switch (entity.Type)
                        {
                            case 1:
                                mail.AddAttachment(EnumCurrencyType.Coin, entity.Count);
                                break;
                            case 2:
                                mail.AddAttachment(EnumCurrencyType.Point, entity.Count);
                                break;
                            case 3:
                                mail.AddAttachment(entity.Count, entity.ItemCode, entity.IsBinding, entity.Strength);
                                break;
                            case 4://声望
                                mail.AddAttachment(EnumCurrencyType.Prestige, entity.Count);
                                break;
                            case 8://绑劵
                                mail.AddAttachment(EnumCurrencyType.BindPoint, entity.Count);
                                break;
                        }
                    }
                }
                var mailInfo = mail.MailInfo;
                string failList = "";
                foreach (var entity in managers)
                {
                    try
                    {
                        var mailEntity = mailInfo.Clone();
                        mailEntity.ManagerId = entity.ManagerId;
                        MailInfoMgr.Insert(mailEntity, null, entity.ZoneId);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Insert(ex);
                        failList = string.Format("{0}{1},{2}|", failList, entity.ZoneId, entity.Account);
                    }

                }
                if (!string.IsNullOrEmpty(failList))
                {
                    ShowMessage("部分发送失败，列表："+failList.TrimEnd('|'));
                }
                else
                {
                    ShowMessage("发送成功");
                }
                
                datagrid1.DataSource = null;
                datagrid1.DataBind();
                LocalAttachment = null;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage("发送失败："+ex.Message);
            }
        }

        void ShowMessage(string msg)
        {
            ltlMessage.Text = msg;
        }

        string _sessionKey="Attachment";
        List<LocalMailAttachmentEntity> LocalAttachment
        {
            get
            {
                if (Session[_sessionKey] != null)
                {
                    var a = Session[_sessionKey] as List<LocalMailAttachmentEntity>;
                    if (a != null)
                        return a;
                }
                return new List<LocalMailAttachmentEntity>(0);
            }
            set { Session[_sessionKey] = value; }
        }
    }

    public class LocalMailManagerCorssEntity
    {
        public string ZoneId { get; set; }

        public Guid ManagerId { get; set; }

        public string Account { get; set; }

        public int Point { get; set; }
    }
}