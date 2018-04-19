using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.NBall.Custom.ItemUsed;

namespace Games.NBall.AdminWeb.Account
{
    public partial class TeammemberInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("球员信息", BindData, ClearData);
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

            ClearData();
            var solution = NbSolutionMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            if (solution == null)
            {
                Master.ShowMessage("没有数据.");
                return;
            }


            var teammembers = TeammemberMgr.GetByManager(accountData.ManagerId, accountData.Mod, accountData.ZoneId);
            if (teammembers != null)
            {
                foreach (var teammember in teammembers)
                {
                    teammember.Name = AdminCache.Instance.GetPlayerName(teammember.PlayerId);
                    teammember.IsMain = solution.PlayerString.Contains(teammember.PlayerId.ToString());
                }
                datagrid1.DataSource = teammembers;
                datagrid1.DataBind();
            }
           
        }

        void ClearData()
        {
            lblHint.Text = "";
            datagrid1.DataSource = null;
            datagrid1.DataBind();
        }

        protected void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var teammember = e.Item.DataItem as TeammemberEntity;
                if (teammember != null)
                {
                    try
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
                        else
                        {
                            lblEquipment.Text = "无";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Teamember读取出错,id:"+teammember.Idx);
                    }
                }
            }
        }
    }
}