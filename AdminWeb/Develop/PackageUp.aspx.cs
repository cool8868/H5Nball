using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.AdminWeb.Entities;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;

namespace Games.NBall.AdminWeb.Develop
{
    public partial class PackageUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("开发人员背包", BindData, ClearData);
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
                    if (item == null || item.Status != (int)EnumItemStatus.Locked)
                    {
                        Master.ShowMessage("该物品不是锁定状态.");
                        return;
                    }
                    item.Status = (int)EnumItemStatus.Normal;
                    var code = package.Update(item);
                    if (code != MessageCode.Success)
                    {
                        Master.ShowMessage("失败，Code：" + code);
                        return;
                    }
                    if (package.Save())
                    {
                        Master.ShowMessage("解锁成功.");
                        package.Shadow.Save();
                    }

                    break;
                case "unlockBinding":
                    var itemId11 = new Guid(e.Item.Cells[0].Text);
                    var package11 = ItemCore.Instance.GetPackage(accountData.ManagerId, EnumTransactionType.AdminUnlockBindItem, accountData.ZoneId);
                    if (package11 == null)
                    {
                        Master.ShowMessage("获取背包信息失败.");
                        return;
                    }
                    var item11 = package11.GetItem(itemId11);
                    if (item11 == null || item11.IsDeal)
                    {
                        Master.ShowMessage("该物品是可出售状态");
                        return;
                    }
                    item11.IsDeal = true;
                    var code11 = package11.Update(item11);
                    if (code11 != MessageCode.Success)
                    {
                        Master.ShowMessage("失败，Code：" + code11);
                        return;
                    }
                    if (package11.Save())
                    {
                        Master.ShowMessage("变成可出售出售成功.");
                        package11.Shadow.Save();
                    }

                    break;
                case "delete":
                    var itemId1 = new Guid(e.Item.Cells[0].Text);
                    var package1 = ItemPackageMgr.GetById(accountData.ManagerId, accountData.ZoneId);
                    if (package1 == null)
                    {
                        Master.ShowMessage("获取背包信息失败.");
                        return;
                    }
                    var packageItemsEntity = SerializationHelper.FromByte<ItemPackageItemsEntity>(package1.ItemString);
                    var items1 = packageItemsEntity.Items.FindAll(d => d.ItemId == itemId1);
                    var shadow1 = new TransactionShadow(accountData.ManagerId, EnumTransactionType.AdminDeleteItem, accountData.ZoneId);
                    foreach (var entity in items1)
                    {
                        if (packageItemsEntity.Items.Remove(entity))
                        {
                            shadow1.AddShadow(entity, EnumOperationType.Delete, entity.ItemCount);
                        }
                        else
                        {
                            Master.ShowMessage("remove 失败.");
                            return;
                        }
                    }
                    package1.ItemString=SerializationHelper.ToByte(packageItemsEntity);
                    ItemPackageMgr.Update(package1, null, accountData.ZoneId);
                    shadow1.Save();
                    BindPackage(package1);
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
                    entity.IsBindingV = itemInfo.IsDeal;
                    entity.ItemCode = itemInfo.ItemCode;
                    entity.ItemCount = itemInfo.ItemCount;
                    entity.Strength = itemInfo.GetStrength();
                    entity.ItemId = itemInfo.ItemId;
                    entity.ItemType = itemInfo.ItemType;
                    entity.ItemTypeV = CacheDataHelper.Instance.GetEnumDescription("EnumItemType", itemInfo.ItemType);
                    entity.ItemProperty = itemInfo.ItemProperty;
                    entity.Status = itemInfo.Status;
                    entity.StatusV = CacheDataHelper.Instance.GetEnumDescription("EnumItemStatus", itemInfo.Status);
                    BuildItemInfo(itemInfo, entity);
                    if (list.Exists(d => d.ItemId == itemInfo.ItemId))
                    {
                        entity.IsRepeat = true;
                    }
                    list.Add(entity);
                }

                lblPackageGrid.Text = string.Format("格数:{0}/{1}", packageData.Items.Count, packageFrame.PackageSize);
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
                    enumName = "EnumEquipmentQuality";
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