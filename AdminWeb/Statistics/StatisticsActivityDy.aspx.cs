using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Bll;
using Games.NBall.Bll.NBall;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;

namespace Games.NBall.AdminWeb.Statistics
{
    public partial class StatisticsActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name, true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                datagridzone.DataSource = null;
                datagrid1.DataSource = null;
                datagrid2.DataSource = null;
                var sitename = AdminMgr.GetSelectZoneId(HttpContext.Current, ddlZone);
                if (sitename == "0")
                {
                    ltlMessage.Text = "暂不支持所有区查询";
                    return;
                }

                var dyStrengthUsers = ActivityDyMgr.GetManagerIdDyStrength(sitename);
                var dyLadderRankUsers = ActivityDyMgr.GetDyLadderRank(sitename);
                var dyPowerRankUser = ActivityDyMgr.GetDyPowerRank(sitename);
                //Dictionary<string, List<ActivityDyUserEntity>> zoneUsers =
                //    new Dictionary<string, List<ActivityDyUserEntity>>();
                //if (dyStrengthUsers.Count > 0)
                //{
                //    foreach (var entity in dyStrengthUsers)
                //    {
                //        if (!zoneUsers.ContainsKey(entity.ZoneId))
                //            zoneUsers.Add(entity.ZoneId, new List<ActivityDyUserEntity>());
                //        zoneUsers[entity.ZoneId].Add(entity);

                //    }
                //}
                //foreach (var entity in zoneUsers)
                //{
                //    for (int i = 0; i < entity.Value.Count; i++)
                //    {
                //        CreateActivityDyUserStrength(entity.Key, entity.Value[i]);
                //        var adEntity = ActivitystatisticsDouyuMgr.GetById(entity.Value[i].ManagerId);
                //        UpdateActivitystatisticsDouyuEntity(entity.Value[i], adEntity);
                //    } 
                //}

                //for (int i = 0; i < dyStrengthUsers.Count; i++)
                //{
                //    CreateActivityDyUserStrength(dyStrengthUsers[i].ZoneId, dyStrengthUsers[i]);
                //    var adEntity = ActivitystatisticsDouyuMgr.GetById(dyStrengthUsers[i].ManagerId);
                //    UpdateActivitystatisticsDouyuEntity(dyStrengthUsers[i], adEntity);
                //}

                datagridzone.DataSource = dyStrengthUsers;
                datagrid1.DataSource = dyLadderRankUsers;
                datagrid2.DataSource = dyPowerRankUser;
                datagridzone.DataBind();
                datagrid1.DataBind();
                datagrid2.DataBind();
                ltlMessage.Text = "";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        //void UpdateActivitystatisticsDouyuEntity(ActivityDyUserEntity user,
        //    ActivitystatisticsDouyuEntity adEntity)
        //{
        //    if (adEntity == null)
        //    {
        //        adEntity = new ActivitystatisticsDouyuEntity();
        //        adEntity.ManagerId = user.ManagerId;
        //        adEntity.ZoneName = user.ZoneName;
        //        adEntity.ZoneId = user.ZoneId;
        //        adEntity.Account = user.Account;
        //        adEntity.ExctingId = user.ExcitingId;
        //        adEntity.CurData = user.Curdata;
        //        adEntity.Status = user.Status;
        //        adEntity.Strength7 = user.Strength7;
        //        adEntity.Strength9 = user.Strength9;
        //        adEntity.UpdateTime = DateTime.Now;
        //        adEntity.RowTime = DateTime.Now;
        //        ActivitystatisticsDouyuMgr.Insert(adEntity);
        //    }
        //    else
        //    {
        //        adEntity.CurData = user.Curdata;
        //        adEntity.Status = user.Status;
        //        adEntity.Strength7 = user.Strength7;
        //        adEntity.Strength9 = user.Strength9;
        //        adEntity.UpdateTime = DateTime.Now;
        //        ActivitystatisticsDouyuMgr.Update(adEntity);
        //    }
        //}

        void CreateActivityDyUserStrength(string zoneId, ActivityDyUserEntity entity)
        {
            var mod = ShareUtil.GetTableMod(entity.ManagerId);
            var list = TeammemberMgr.GetByManager(entity.ManagerId, mod, zoneId);
            if (list.Count > 0)
            {
                foreach (var teammemberEntity in list)
                {
                    var playercardEntity = SerializationHelper.FromByte<PlayerCardUsedEntity>(teammemberEntity.UsedPlayerCard);
                    if (playercardEntity != null)
                    {
                        if (playercardEntity.Property.Strength >= 7)
                        {
                            entity.Strength7++;
                            if (playercardEntity.Property.Strength == 9)
                                entity.Strength9++;
                        }
                    }
                }
            }
            var package = ItemCore.Instance.GetPackageWithoutShadow(entity.ManagerId, zoneId);
            var items = package.GetItemsByType((int)EnumItemType.PlayerCard);
            foreach (var item in items)
            {
                int strength = item.GetStrength();
                if (strength >= 7)
                {
                    entity.Strength7++;
                    if (strength == 9)
                        entity.Strength9++;
                }
            }
        }


        private static int _index = 0;
        void ShowMessage(string msg)
        {
            _index++;
            ltlMessage.Text = "(序列:" + _index + ")" + msg;
        }

        private List<TeammemberEntity> _managerTeammember = new List<TeammemberEntity>();
        

    }
}