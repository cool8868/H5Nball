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
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.ItemUsed;

namespace Games.NBall.AdminWeb.Account
{
    public partial class AccountInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("经理信息",BindData,ClearData);
            if (!IsPostBack)
            {
                BindData();
            }
        }

        void BindData()
        {
            var accountData = Master.GetAccount();
            if (accountData == null)
            {
                Master.ShowMessage("请先选择经理.");
                return;
            }
            var manager = NbManagerMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            if (manager == null)
            {
                Master.ShowMessage("获取经理信息失败.");
                return;
            }
            ClearData();
            lblCoin.Text = manager.Coin.ToString();
            lblExp.Text = manager.EXP.ToString();
            lblLevel.Text = manager.Level.ToString();
            lblLogo.Text = manager.Logo.ToString();
            lblName.Text = manager.Name;
            lblReiki.Text = manager.Reiki.ToString();
            lblSophisticate.Text = manager.Sophisticate.ToString();
            lblStatus.Text = CacheDataHelper.Instance.GetEnumDescription("EnumManagerStatus",manager.Status);
            lblSubstitute.Text = (manager.TeammemberMax-11).ToString();
            lblVipLevel.Text = manager.VipLevel.ToString();

            var payAccount = PayUserMgr.GetById(accountData.Account, accountData.ZoneId);
            if (payAccount != null)
            {
                lblCash.Text = payAccount.TotalCash.ToString();
                lblPoint.Text = string.Format("{0}+{1}(赠送)={2}",payAccount.Point,payAccount.Bonus,payAccount.TotalPoint);
            }

            var user = NbUserMgr.GetById(accountData.Account,accountData.ZoneId);
            if (user != null)
            {
                lblLastLoginTime.Text = user.LastLoginTime.ToString(AdminMgr.TimeFormat);
                lblRegisterTime.Text = user.RowTime.ToString(AdminMgr.TimeFormat);
                lblLoginday.Text = user.ContinuingLoginDay.ToString();
            }
            var onlineEntity = OnlineInfoMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            if (onlineEntity != null)
            {
                lblOnlineStatus.Text = onlineEntity.IsOnline ? "在线" : "不在线";
                var total = onlineEntity.RealTotalMinutes;
                var day = total/1440;
                var hour = (total%1440)/60;
                var minute = ((total%1440)%60);
                lblOnlineTotal.Text =string.Format("{0} 天 {1} 小时 {2} 分钟",day,hour,minute);
                lblOnlineCur.Text = onlineEntity.TodayMinutes + " 分钟";
            }
            else
            {
                lblOnlineStatus.Text = "无记录";
            }
            var solution = NbSolutionMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            string playerString = "";
            if (solution != null)
            {
                var formation = CacheFactory.FormationCache.GetFormation(solution.FormationId);
                lblSolution.Text = formation==null?solution.FormationId.ToString():formation.Name;
                lblSolutionPlayer.Text = solution.PlayerString;
                lblSolutionSkill.Text = solution.SkillString;
                playerString = solution.PlayerString;
            }

            var managerExtra = NbManagerextraMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            if (managerExtra != null)
            {
                ManagerUtil.CalCurrentStamina(managerExtra,manager.Level,manager.VipLevel);
                lblStamina.Text = managerExtra.Stamina.ToString();
            }

            var teammembers = TeammemberMgr.GetByManager(accountData.ManagerId, accountData.Mod, accountData.ZoneId);
            if (teammembers != null)
            {
                foreach (var teammember in teammembers)
                {
                    teammember.Name = AdminCache.Instance.GetPlayerName(teammember.PlayerId);
                    teammember.IsMain = playerString.Contains(teammember.PlayerId.ToString());
                }
                datagrid1.DataSource = teammembers;
                datagrid1.DataBind();
            }

            var list = FriendinviteMgr.InviteManagerList(manager.Account, accountData.ZoneId);
           if (list != null)
           {
               var friendInviteString = "";
               foreach (var item in list)
               {
                   friendInviteString += item.Name + ",";
               }
               lblFriendInvite.Text = friendInviteString;
           }
        }

        void ClearData()
        {
            lblCash.Text = "";
            lblCoin.Text = "";
            lblExp.Text = "";
            lblLastLoginTime.Text = "";
            lblLevel.Text = "";
            lblLoginday.Text = "";
            lblLogo.Text = "";
            lblName.Text = "";
            lblPoint.Text = "";
            lblRegisterTime.Text = "";
            lblReiki.Text = "";
            lblSolution.Text = "";
            lblSolutionPlayer.Text = "";
            lblSolutionSkill.Text = "";
            lblSophisticate.Text = "";
            lblStamina.Text = "";
            lblStatus.Text = "";
            lblSubstitute.Text = "";
            lblVipLevel.Text = "";
        }

        protected void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var teammember = e.Item.DataItem as TeammemberEntity;
                if (teammember != null)
                {
                    var lblPlayerCard = e.Item.FindControl("lblPlayerCard") as Label;
                    var strength = teammember.Strength;
                    if (strength > 0)
                    {
                        lblPlayerCard.Text = "+" + strength;
                    }
                    else
                    {
                        lblPlayerCard.Text = "无";
                    }
                    var lblEquipment = e.Item.FindControl("lblEquipment") as Label;
                    if (teammember.UsedEquipment != null && teammember.UsedEquipment.Length > 0)
                    {
                        try
                        {
                            var equipment = SerializationHelper.FromByte<EquipmentUsedEntity>(teammember.UsedEquipment);
                            var itemDic = AdminCache.Instance.GetItem(equipment.ItemCode);
                            if (itemDic != null)
                            {
                                var ename = itemDic.ItemName;
                                var equality = CacheDataHelper.Instance.GetEnumDescription("EnumEquipmentQuality", itemDic.EquipmentQuality);
                                var eProperty1 = AdminMgr.BuildPropertyPlus(equipment.Property.PropertyPluses[0]);
                                var eProperty2 = AdminMgr.BuildPropertyPlus(equipment.Property.PropertyPluses[1]);
                                lblEquipment.Text = string.Format("[{0}]{1}({2},{3})", equality, ename, eProperty1,
                                                                  eProperty2);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Insert(string.Format("teammember equipment data error,idx:{0}",teammember.Idx),LogType.Info);
                            throw ex;
                        }
                        
                    }
                    else
                    {
                        lblEquipment.Text = "无";
                    }
                }
            }
        }
    }
}