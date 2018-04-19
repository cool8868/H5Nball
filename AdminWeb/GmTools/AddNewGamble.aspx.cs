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
    public partial class AddNewGamble : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckInput())
                    return;
                string homeName = txtHomeName.Text.Trim();
                string awayName = txtAwayName.Text.Trim();
                DateTime startTime = Convert.ToDateTime(txtStartTime.Text);
                EuropeMatchEntity entity = new EuropeMatchEntity(0, homeName, awayName, startTime.Date, startTime, 0, 0,
                    0,
                    1, DateTime.Now, DateTime.Now);
                if (!EuropeMatchMgr.Insert(entity))
                    ShowMessage("发布失败");
                else
                {
                    ShowMessage("发布成功");
                    BindData();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("发布失败:"+ex);
            }
            txtHomeName.Text = "";
            txtStartTime.Text = "";
            txtAwayName.Text = "";
        }

        protected void btn_SendPrize_Click(object sender, EventArgs e)
        {
            int matchId = ConvertHelper.ConvertToInt(txtSendMatchId.Text.Trim());
            if (matchId == 0)
            {
                ShowMessage("比赛ID不能为0");
                return;
            }
            var allzone = AllZoneinfoMgr.GetAll();
            foreach (var item in allzone)
            {
                SendPrize(matchId,item.ZoneName);
            }
            ShowMessage("发奖完成");
        }

        protected void btnEnd_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                int matchId = ConvertHelper.ConvertToInt(txtMatchId.Text.Trim());
                int homeGoals = ConvertHelper.ConvertToInt(txtHomeGoals.Text.Trim());
                int awayGoals = ConvertHelper.ConvertToInt(txtAwayGoals.Text.Trim());
                if (matchId == 0)
                {
                    ShowMessage("比赛ID不能为0");
                    return;
                }

                var match = EuropeMatchMgr.GetById(matchId);
                if (match == null)
                {
                    ShowMessage("没有找到比赛");
                    return;
                }
                //if (match.MatchTime.AddHours(2) > date)
                //{
                //    ShowMessage("开始时间不足两小时");
                //    return;
                //}
                if (match.States != (int) EnumEuropeStatus.MatchIng)
                {
                    ShowMessage("比赛还未开始");
                    return;
                }
                match.HomeGoals = homeGoals;
                match.AwayGoals = awayGoals;
                if (homeGoals > awayGoals)
                    match.ResultType = 1;
                else if (homeGoals < awayGoals)
                    match.ResultType = 3;
                else
                    match.ResultType = 2;
                match.States = (int) EnumEuropeStatus.MatchEnd;
                if (!EuropeMatchMgr.Update(match))
                {
                    ShowMessage("更新失败");
                    return;
                }
                BindData();
                ShowMessage("成功");
            }
            catch (Exception ex)
            {
                ShowMessage("失败:" + ex.ToString());
            }
            txtMatchId.Text = "";
            txtHomeGoals.Text = "";
            txtAwayGoals.Text = "";
        }

        bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtHomeName.Text.Trim()))
            {
                ShowMessage("请输入主队名");
                return false;
            }
            if (string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                ShowMessage("请输入开始时间");
                return false;
            }
            if (string.IsNullOrEmpty(txtAwayName.Text.Trim()))
            {
                ShowMessage("请输入客队名");
                return false;
            }
            return true;
        }

        void ShowMessage(string msg)
        {
            ltlMessage.Text = msg;
        }


        protected void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
            }
        }

        private void BindData()
        {

            ClearData(); 
            DateTime date = DateTime.Now;
            //排除1个月之前的比赛
            var matchList = EuropeMatchMgr.GetAllMatvch(date.AddDays(-30));
            if (matchList == null)
                matchList = new List<EuropeMatchEntity>();
            matchList = matchList.OrderByDescending(r => r.MatchId).ToList();
            foreach (var item in matchList)
            {

                if (item != null)
                {
                    try
                    {
                        switch (item.ResultType)
                        {
                            case 0:
                                item.ResultTypeString = "初始";
                                break;
                            case 1:
                                item.ResultTypeString = "主队获胜";
                                break;
                            case 2:
                                item.ResultTypeString = "平";
                                break;
                            case 3:
                                item.ResultTypeString = "客队获胜";
                                break;
                        }
                        switch (item.States)
                        {
                            case 0:
                                item.StatusString = "初始";
                                break;
                            case 1:
                                item.StatusString = "可竞猜";
                                break;
                            case 2:
                                item.StatusString = "比赛中";
                                break;
                            case 3:
                                item.StatusString = "比赛完成";
                                break;
                            case 4:
                                item.StatusString = "发奖完成";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("match读取出错,id:" + item.MatchId);
                    }
                }
            }
            datagrid1.DataSource = matchList;
            datagrid1.DataBind();

        }

        void ClearData()
        {
            datagrid1.DataSource = null;
            datagrid1.DataBind();
        }

        public void SendPrize( int matchId,string zoneId)
        {
            DateTime date = DateTime.Now;
            var match = EuropeMatchMgr.GetById(matchId);
            var _season = EuropeSeasonMgr.GetSeason(date,zoneId);
            if (match == null)
                return;
            var season = EuropeSeasonMgr.GetSeason(date,zoneId);
            //获取未发奖的竞猜
            var sendPrizeList = EuropeGamblerecordMgr.GetNotPrize(matchId, zoneId);
            foreach (var item in sendPrizeList)
            {
                if (item.IsSendPrize)
                    continue;
                item.IsSendPrize = true;
                item.UpdateTime = date;
                MailBuilder mail = null;
                EuropeGambleEntity gambleInfo = null;
                bool isInsertInfo = false;
                if (item.GambleType == match.ResultType) //竞猜正确
                {
                    item.IsGambleCorrect = true;
                    item.ReturnPoint = item.Point*2;
                    //发邮件
                    mail = new MailBuilder(item.ManagerId, EnumMailType.Europe, item.ReturnPoint, match.HomeName,
                        match.AwayName);
                    gambleInfo = EuropeGambleMgr.GetById(item.ManagerId, zoneId);
                    if (gambleInfo == null)
                    {
                        isInsertInfo = true;
                        gambleInfo = new EuropeGambleEntity(item.ManagerId, 1, "0,0,0,0", date, date, season.Idx);
                    }
                    else
                    {
                        if (_season!=null && gambleInfo.SeasonId != _season.Idx)
                        {
                            //插入记录
                            EuropeRecordMgr.Insert(new EuropeRecordEntity(0, gambleInfo.ManagerId, gambleInfo.SeasonId,
                                gambleInfo.CorrectNumber, gambleInfo.PrizeRecord, date), null, zoneId);
                            //更新活动
                            gambleInfo.CorrectNumber = 0;
                            gambleInfo.PrizeRecord = "0,0,0,0";
                            gambleInfo.SeasonId = _season.Idx;
                        }
                        gambleInfo.CorrectNumber++;
                        gambleInfo.UpdateTime = date;
                    }
                }
                else
                {
                    item.IsGambleCorrect = false;
                }

                MessageCode code = MessageCode.FailUpdate;
                if (mail != null)
                {
                    if (!mail.Save(zoneId))
                    {
                        ShowMessage("发送邮件失败");
                        return;
                    }
                }
                if (gambleInfo != null)
                {
                    if (isInsertInfo)
                    {
                        if (!EuropeGambleMgr.Insert(gambleInfo,null, zoneId))
                            code = MessageCode.NbUpdateFail;
                    }
                    else
                    {
                        if (!EuropeGambleMgr.Update(gambleInfo,null, zoneId))
                            code = MessageCode.NbUpdateFail;
                    }
                }
                if (!EuropeGamblerecordMgr.Update(item, null, zoneId))
                    code = MessageCode.NbUpdateFail;
                if(code != MessageCode.Success)
                    ShowMessage("保存失败");
            }
        }
    }
}