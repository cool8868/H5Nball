using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Constellation;
using Games.NBall.Core.CrossCrowd;
using Games.NBall.Core.CrossLadder;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Develop
{
    public partial class TestArena : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name);
            }
        }

        protected void bt_ArenaAwary_Click(object sender, EventArgs e)
        {
        //    ShowMessage("竞技场后台发奖中......");
        //    var zoneId = AdminMgr.GetSelectZoneId(HttpContext.Current,ddlZone);

        //    //获取数据
        //    ArenaMainEntity entityMain = ArenaMainMgr.C_ArenaSeason(zoneId);
        //    if (entityMain.TheLastSeason.Date <= DateTime.Today)
        //    {
        //        bool result = ArenaMainMgr.C_ArenaEveryThreeDays(null,zoneId);
        //        if (!result)
        //            SystemlogMgr.Error("Arena.EveryThreeDays", "竞技场计划任务执行不成功");
        //    }
        //    MessageCode code = ArenaMianCore.Instance.ExamineNoPrize(zoneId);
        //    if(code == MessageCode.Success)
        //        ShowMessage("竞技场后台发奖完成");
        //    else
        //        ShowMessage("竞技场后台发奖有误");
        }

        protected void btnLadderPrize_Click(object sender, EventArgs e)
        {
            try
            {
                var zone = AdminMgr.GetSelectZoneId(HttpContext.Current, ddlZone);
                var curSeason = ConvertHelper.ConvertToInt(txtLadderSeason.Text);
                if (curSeason <= 0)
                {
                    ShowMessage("天梯赛季不能小于1");
                    return;
                }
                ShowMessage("天梯发奖开始...");
                var managers = LadderManagerhistoryMgr.GetPrizeManager(curSeason, zone);
                if (managers != null)
                {
                    List<MailBuilder> mails = new List<MailBuilder>(managers.Count);
                    foreach (var manager in managers)
                    {
                        LadderThread.Instance.SendPrize(manager, 0, ref mails);
                    }

                    var mailDataTable = MailCore.BuildMailBulkTable(mails);
                    LadderSqlHelper.SaveManagerPrize(managers, mailDataTable, zone);
                }
                ShowMessage("天梯发奖成功。");
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage("local exception:" + ex.Message);
            }
        }
        protected void btnCrossLadderPrize_Click(object sender, EventArgs e)
        {
            try
            {
                var domain = ConvertHelper.ConvertToInt(txtDomain.Text);
                if (domain <= 0)
                {
                    ShowMessage("domain不能小于1");
                    return;
                }
                var curSeason = ConvertHelper.ConvertToInt(txtCrossLadderSeason.Text);

                if (curSeason <= 0)
                {
                    DateTime date = DateTime.Parse(txtCrossLadderSeason.Text.Trim());

                    ShowMessage("跨服天梯发奖开始..." + date);
                    CrossLadderThread.ReSendDailyPrize(date, 0, domain);
                    ShowMessage("跨服天梯发奖成功。");
                }
                else
                {

                    ShowMessage("跨服天梯发奖开始...");
                    CrossLadderThread.ReSendSeasonPrize(curSeason, domain);

                    ShowMessage("跨服天梯发奖成功。");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage("local exception:" + ex.Message);
            }
        }

        protected void BtnCrossLadderDailPrize_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        var domain = ConvertHelper.ConvertToInt(txtDomain.Text);
        //        if (domain <= 0)
        //        {
        //            ShowMessage("domain不能小于1");
        //            return;
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Insert(ex);
        //        ShowMessage("local exception:" + ex.Message);
        //    }
        }

        protected void btnCrowdPrize_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        var zone = AdminMgr.GetSelectZoneId(HttpContext.Current,ddlZone);
        //        var curSeason = ConvertHelper.ConvertToInt(txtCrowdId.Text);
        //        if (curSeason <= 0)
        //        {
        //            ShowMessage("群雄id不能小于1");
        //            return;
        //        }
        //        ShowMessage("群雄发奖开始...");
        //        var s = WebServerHandler.CrowdSendPrize(zone, curSeason);
        //        ShowMessage("群雄发奖结果："+s);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Insert(ex);
        //        ShowMessage("local exception:" + ex.Message);
        //    }
        }

        protected void btn_activityex_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        var zone = AdminMgr.GetSelectZoneId(HttpContext.Current, ddlZone);
              


        //        ShowMessage("开始发奖...");
        //        MessageCode mess = BackRunSend(zone);
        //        if (mess != MessageCode.Success)
        //            ShowMessage(mess.ToString());
        //        else
        //            ShowMessage("发奖完成。");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Insert(ex);
        //        ShowMessage("local exception:" + ex.Message);
        //    }
        }

        protected void btnCrowdStart_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        var zone = AdminMgr.GetSelectZoneId(HttpContext.Current,ddlZone);
        //        var starTime = Convert.ToDateTime(txtStartTime.Value);
        //        var endTime = Convert.ToDateTime(txtEndTime.Value);
        //        if (starTime >= endTime)
        //        {
        //            ShowMessage("开始时间不能大于结束时间");
        //            return;
        //        }
        //        ShowMessage("创建开始...");
        //        var s = WebServerHandler.CrowdStart(zone, starTime, endTime);
        //        ShowMessage("创建结果："+s);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Insert(ex);
        //        ShowMessage("local exception:" + ex.Message);
        //    }
        }

        protected void btnCrossCrowdSendPrize_Click(object sender, EventArgs e)
        {
            try
            {
                var crowdId = ConvertHelper.ConvertToInt(txtCrossCrowdId.Text);
                if (crowdId <= 0)
                {
                    ShowMessage("群雄id不能小于1");
                    return;
                }
                ShowMessage("群雄发奖开始...");
                var s = CrossCrowdThread.AdminSendPrize(crowdId) ;
                ShowMessage("群雄发奖结果："+s);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage("local exception:" + ex.Message);
            }
        }
        

        void ShowMessage(string msg)
        {
            ltlMessage.Text += string.Format("<div>{0}</div>", msg);
        }

        protected void btn_AdPrize_Click(object sender, EventArgs e)
        { 
        }

        protected void btnLadderPrizeMergeZone_Click(object sender, EventArgs e)
        {
        }

        protected void btc_constellation_Click(object sender, EventArgs e)
        {
        }

        protected void btnChampionMatch1_Click(object sender, EventArgs e)
        {
        }

        protected void btnCrossLadderCoin_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMessage("开始补发天梯币...");
                var list = CrossladderManagerMgr.GetDailyHonor();
                if (list != null)
                {
                    foreach (var entity in list)
                    {
                        LadderManagerMgr.AddDailyHonor(entity.ManagerId, entity.NewlyHonor, entity.NewlyLadderCoin, null,
                            entity.SiteId);
                    }
                }
                ShowMessage("补发成功...");
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SendDailyHonor", ex);
            }
        }
    }
}