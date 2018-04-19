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

namespace Games.NBall.AdminWeb.Tools
{
    public partial class SendMail : System.Web.UI.Page
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
            datagrid1.DataSource = null;
            datagrid1.DataBind();


            List<StatusList> lists = new List<StatusList>();
            lists.Add(new StatusList("1", "普通邮件"));
            lists.Add(new StatusList("2", "共享邮件"));
            dr_type.DataTextField = "Text";
            dr_type.DataValueField = "Value";
            dr_type.DataSource = lists;
            dr_type.DataBind();
            dr_type.SelectedIndex = 0;
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var zoneId = ZoneId;
                if (string.IsNullOrEmpty(zoneId))
                {
                    ShowMessage("请选择区");
                    return;
                }
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
                List<NbManagerEntity> managers = new List<NbManagerEntity>();
                var ss = users.Split(',');
                foreach (var s in ss)
                {
                    var manager =NbManagerMgr.GetByAccount(s,zoneId);
                    if (manager == null || manager.Count<1)
                    {
                        ShowMessage("未找到对应经理，账号:"+s);
                        return;
                    }
                    managers.Add(manager[0]);
                }
                var mailType = ConvertHelper.ConvertToInt(dr_type.SelectedValue);
                var attachments = GetAttachments();
                if (mailType == 1)
                {
                MailBuilder mail = new MailBuilder(title,content);
               
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
                    List<MailInfoEntity> mailList = new List<MailInfoEntity>(managers.Count);
                    var mailInfo = mail.MailInfo;
                    foreach (var entity in managers)
                    {
                        var mailEntity = mailInfo.Clone();
                        mailEntity.ManagerId = entity.Idx;
                        mailList.Add(mailEntity);
                    }
                    var mailTable = MailCore.BuildMailBulkTable(mailList);
                    if (MailSqlHelper.SaveMailBulk(mailTable,
                        ConnectionFactory.Instance.GetConnectionString(zoneId, "Main")))
                    {
                        ShowMessage("发送成功");
                        datagrid1.DataSource = null;
                        datagrid1.DataBind();
                        LocalAttachment = null;
                    }
                    else
                    {
                        ShowMessage("发送失败");
                    }
                }
                else if (mailType == 2)
                //{
                //    MailShareBuilder mail = new MailShareBuilder(title, content);

                //    if (attachments.Count > 0)
                //    {
                //        foreach (var entity in attachments)
                //        {
                //            switch (entity.Type)
                //            {
                //                case 1:
                //                    mail.AddAttachment(EnumCurrencyType.Coin, entity.Count);
                //                    break;
                //                case 2:
                //                    mail.AddAttachment(EnumCurrencyType.Point, entity.Count);
                //                    break;
                //                case 3:
                //                    mail.AddAttachment(entity.Count, entity.ItemCode, entity.IsBinding, entity.Strength);
                //                    break;
                //                case 4://声望
                //                    mail.AddAttachment(EnumCurrencyType.Prestige, entity.Count);
                //                    break;
                //                case 8://绑劵
                //                    mail.AddAttachment(EnumCurrencyType.BindPoint, entity.Count);
                //                    break;
                //            }
                //        }
                //    }
                //    MailshareInfoEntity mailList = new MailshareInfoEntity();
                //    var mailInfo = mail.MailInfo;
                //    if (managers.Count > 1)
                //    {
                //        ShowMessage("只支持单个发送");
                //        return;
                //    }
                //    foreach (var entity in managers)
                //    {
                //        var mailEntity = mailInfo.Clone();
                //        mailEntity.Account = entity.Account;
                //    }

                //    if (mail.Save(zoneId))
                //    {
                //        ShowMessage("发送成功");
                //        datagrid1.DataSource = null;
                //        datagrid1.DataBind();
                //        LocalAttachment = null;
                //    }
                //    else
                //    {
                //        ShowMessage("发送失败");
                //    }
                //}
                
                    ShowMessage("请选择邮件类型");
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

    public class LocalMailAttachmentEntity
    {
        public LocalMailAttachmentEntity(int type,string typeStr,int count,string name,int itemCode,int strength,bool isBinding)
        {
            this.Type = type;
            this.TypeStr = typeStr;
            this.Count = count;
            this.Name = name;
            this.ItemCode = itemCode;
            this.Strength = strength;
            this.IsBinding = isBinding;
        }

        public int Type { get; set; }

        public string TypeStr { get; set; }

        public int Count { get; set; }

        public string Name { get; set; }

        public int ItemCode { get; set; }

        public int Strength { get; set; }

        public bool IsBinding { get; set; }
    }
}