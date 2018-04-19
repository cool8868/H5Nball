using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.AdminWeb.Entities;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;

namespace Games.NBall.AdminWeb.Account
{
    public partial class PackageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("背包信息", BindData, ClearData);
            if (!IsPostBack)
            {
                //List<StatusList> list = CacheDataHelper.Instance.GetEnumData("EnumItemType");
                //ddlItemType.Items.Add(new ListItem("所有","0"));
                //foreach (var entity in list)
                //{
                //    ddlItemType.Items.Add(new ListItem(entity.Text,entity.Value));
                //}
                //ddlItemType.SelectedIndex = 0;
                BindData();
            }
        }

        public void ItemsGrid_Command(Object sender, DataGridCommandEventArgs e)
        {
            var accountData = Master.GetAccount();
            if (accountData == null)
                return;
            var command = e.CommandName;
            switch (command)
            {
                case "unlock":
                    var itemId = new Guid(e.Item.Cells[0].Text);
                    var package = ItemCore.Instance.GetPackage(accountData.ManagerId, EnumTransactionType.AdminUnlockItem, accountData.ZoneId);
                    if (package == null)
                    {
                        Master.ShowMessage("获取背包信息失败.");
                        return;
                    }
                    var item = package.GetItem(itemId);
                    if (item == null || item.Status != (int) EnumItemStatus.Locked)
                    {
                        Master.ShowMessage("该物品不是锁定状态.");
                        return;
                    }
                    item.Status = (int)EnumItemStatus.Normal;
                    var code =package.Update(item);
                    if (code != MessageCode.Success)
                    {
                        Master.ShowMessage("失败，Code："+code);
                        return;
                    }
                    if (package.Save())
                    {
                        Master.ShowMessage("解锁成功.");
                        package.Shadow.Save();
                    }
                    
                    break;
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
            var package = ItemPackageMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            if (package == null)
            {
                Master.ShowMessage("获取背包信息失败.");
                return;
            }
            
            ClearData();
            BindPackage(package);
        }

        void BindPackage(ItemPackageEntity packageFrame)
        {
            var list = new List<AdminItemInfoEntity>();
            var packageData = SerializationHelper.FromByte<ItemPackageItemsEntity>(packageFrame.ItemString);
            if (packageData.Items != null)
            {
                foreach (var itemInfo in packageData.Items)
                {
                    AdminItemInfoEntity entity = new AdminItemInfoEntity();
                    entity.GridIndex = itemInfo.GridIndex;
                    entity.IsBinding = itemInfo.IsBinding;
                    entity.IsBindingV = itemInfo.IsBinding;
                    entity.ItemCode = itemInfo.ItemCode;
                    entity.ItemCount = itemInfo.ItemCount;
                    entity.Strength = itemInfo.GetStrength();
                    entity.ItemId = itemInfo.ItemId;
                    entity.ItemType = itemInfo.ItemType;
                    entity.ItemTypeV = CacheDataHelper.Instance.GetEnumDescription("EnumItemType", itemInfo.ItemType);
                    entity.ItemProperty = itemInfo.ItemProperty;
                    entity.Status = itemInfo.Status;
                    entity.StatusV = CacheDataHelper.Instance.GetEnumDescription("EnumItemStatus",itemInfo.Status);
                    BuildItemInfo(itemInfo, entity);
                    if (list.Exists(d => d.ItemId == entity.ItemId))
                    {
                        var itemInfobase = list.Find(d => d.ItemId == entity.ItemId && !d.IsRepeat);
                        if (itemInfobase != null)
                            itemInfobase.IsRepeat = true;
                        entity.IsRepeat = true;
                    }
                    list.Add(entity);
                }
                lblPackageGrid.Text = string.Format("格数:{0}/{1}", packageData.Items.Count, packageFrame.PackageSize);
            }
            var list2 = list.FindAll(d => d.IsRepeat);
            if (list2 != null && list2.Count > 0)
            {
                table1.Visible = true;
                DataGrid1.Visible = true;
                DataGrid1.DataSource = list2;
                DataGrid1.DataBind();
            }
            else
            {
                table1.Visible = false;
                DataGrid1.Visible = false;
            }
            dgPackage.DataSource = list;
            dgPackage.DataBind();
        }

        void BuildItemInfo(ItemInfoEntity itemInfo, AdminItemInfoEntity wpfItemInfo)
        {
            var itemCode = itemInfo.ItemCode;
            string enumName = "";
            switch (itemInfo.ItemType)
            {
                case (int)EnumItemType.PlayerCard:
                    enumName = "EnumPlayerCardLevel";
                    break;
                case (int)EnumItemType.Equipment:
                    enumName="EnumEquipmentQuality";
                    break;
                case (int)EnumItemType.BallSoul:
                    enumName = "EnumBallSoulColor";
                    break;
                case (int)EnumItemType.MallItem:
                    enumName = "EnumMallType";
                    break;
                case (int)EnumItemType.SuitDrawing:
                    enumName = "";
                    break;
            }
            var itemDic = AdminCache.Instance.GetItem(itemCode);
            if (itemDic != null)
            {
                wpfItemInfo.Name = itemDic.ItemName;
                if (!string.IsNullOrEmpty(enumName))
                {
                    wpfItemInfo.SubTypeV = CacheDataHelper.Instance.GetEnumDescription(enumName, itemDic.SubType);
                }
            }
        }

        void ClearData()
        {
            lblPackageGrid.Text = "";
            dgPackage.DataSource = null;
            dgPackage.DataBind();
        }
    }
}