using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Share;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.NB_Web.Helper;
using Games.NBall.ServiceContract.Client;
using Games.NBall.UAFacade;
using Games.NBall.WebClient.Weibo;
using Games.NBall.WebServerFacade;

namespace Games.NBall.NB_Web.Command
{
    public class ItemCommand : BaseCommand
    {
        public void Dispatch(string action)
        {
            if (false)
            {
                if (ValidatorAccountOnly() == false)
                    return;
            }
            else
            {
                if (Validator() == false)
                    return;
            }
            switch (action)
            {
                case "getpackage":
                    GetPackage();
                    break;
                case "getpackage2":
                    GetPackage2();
                    break;
                case "arrange":
                    Arrange();
                    break;
                case "deleteitem":
                    DeleteItem();
                    break;
                case "st":
                    Split();
                    break;
                case "stpp":
                    StrengthenParam();
                    break;
                case "stp":
                    Strengthen();
                    break;
                case "dp":
                    Decompose();
                    break;
                case "sts":
                    SynthesisNew();
                    break;
                case "stsp":
                    SynthesisParamNew();
                    break;
                case "tcsp":
                    TheContractSyntheticParam();
                    break;
                case "tcs":
                    TheContractSynthetic();
                    break;
                case "eqsp":
                    EquipmentSynthesisParam();
                    break;
                case "eqs":
                    EquipmentSynthesis();
                    break;
                case "eqse":
                    EquipmentSell();
                    break;
                case "pse":
                    PrpoSell();
                    break;
                case"ei":
                    Exchange();
                    break;
                case "ptp":
                    PlayerUpgradeTheStarParam();
                    break;
                case "pt":
                    PlayerUpgradeTheStar();
                    break;
                case "rpr":
                    ResetPotentialParam();
                    break;
                case "rp":
                    ResetPotential();
                    break;
                default:
                    OutputHelper.OutputBadRequest();
                    break;
            }
        }

        private readonly ItemClient reader = new ItemClient();

        void GetPackage()
        {
            var response = reader.GetPackage(UserAccount.ManagerId);
            OutputPackage(response);
        }
        void GetPackage2()
        {
            var response = reader.GetPackage(UserAccount.ManagerId);
            OutputPackage2(response);
        }
       
        void Arrange()
        {
            var response = reader.Arrange(UserAccount.ManagerId);
            OutputHelper.Output(response);
        }

        void DeleteItem()
        {
            Guid itemId = GetParamGuid("i");
            if (!CheckParam(itemId))
            {
                return;
            }
            int count = GetParamInt("c");
            if (count <= 0)
                count = 1;
            var response = reader.DeleteItem(UserAccount.ManagerId, itemId, count);
            OutputHelper.Output(response);
        }

        void Split()
        {
            Guid itemId = GetParamGuid("i");
            if (!CheckParam(itemId))
            {
                return;
            }
            int count = GetParamInt("c");
            if (!CheckParam(count))
            {
                return;
            }
            var response = reader.SplitItem(UserAccount.ManagerId, itemId, count);
            OutputHelper.Output(response);
        }

        void OutputPackage2(ItemPackageResponse packageResponse)
        {
            if (packageResponse.Code == (int)MessageCode.Success)
            {
                var buffer = GetPackageBuffer(packageResponse);
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
                HttpContext.Current.Response.BinaryWrite(buffer);
            
            }
            else
            {
                OutputHelper.Output(packageResponse.Code);
            }
        }

        void OutputPackage(ItemPackageResponse packageResponse)
        {
            if (packageResponse.Code == (int)MessageCode.Success)
            {
                var response = new ItemPackageDataResponse();
                response.Data = new ItemPackageData();
                //var packageItemsEntity = SerializationHelper.FromByte<ItemPackageItemsEntity>(packageResponse.Data.ItemString);
                //if (packageItemsEntity == null || packageItemsEntity.Items == null)
                //    response.Data.Items = new List<ItemInfoEntity>();
                //else
                //{
                //    response.Data.Items = packageItemsEntity.Items;
                //}
                response.Data.Items = packageResponse.Data.Items;
                response.Data.PackageSize = packageResponse.Data.PackageSize;
                //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
                //var json = SerializationHelper.ToJson(response);
                //var bytes = SerializationHelper.ToByte(json);
                //HttpContext.Current.Response.BinaryWrite(bytes);
                OutputHelper.Output(response);
            }
            else
            {
                OutputHelper.Output(packageResponse.Code);
            }
        }

        #region WritePackage
        public byte[] GetPackageBuffer(ItemPackageResponse response)
        {
            using (var ms = new MemoryStream())
            {
                var writer = new BinaryWriter(ms);
                var packageData = response.Data;
                ByteWriter.WriteTo(writer, packageData.PackageSize);//32位 int
                ByteWriter.WriteTo(writer, packageData.Items.Count);//32位 int
                foreach (var entity in packageData.Items)
                {
                    ByteWriter.WriteTo(writer, entity.ItemCount);//32位 int
                    ByteWriter.WriteTo(writer, entity.ItemCode);//32位 int
                    ByteWriter.WriteTo(writer, entity.ItemId);//string
                    ByteWriter.WriteTo(writer, (byte)entity.ItemType);//8位 byte
                    switch (entity.ItemType)
                    {
                        case (int)EnumItemType.PlayerCard:
                            WritePlayerCardProperty(writer, entity.ItemProperty as PlayerCardProperty);
                            break;
                        case (int)EnumItemType.Equipment:
                            WriteEquipmentProperty(writer, entity.ItemProperty as EquipmentProperty);
                            break;
                    }
                }

                writer.Flush();
                var bytes = ms.ToArray();
                return bytes;
            }
        }

        void WritePlayerCardProperty(BinaryWriter writer, PlayerCardProperty property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            ByteWriter.WriteTo(writer, property.Exp);
            ByteWriter.WriteTo(writer, property.IsMain);
            ByteWriter.WriteTo(writer, property.IsTrain);
            ByteWriter.WriteTo(writer, property.Level);
            ByteWriter.WriteTo(writer, property.Strength);
            ByteWriter.WriteTo(writer, property.TeammemberId);
            ByteWriter.WriteTo(writer, property.TheActualKpi);
            WriteEquipmentUsed(writer, property.Equipment);

        }

        void WriteEquipmentUsed(BinaryWriter writer, EquipmentUsedEntity property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            ByteWriter.WriteTo(writer, property.ItemId);
            ByteWriter.WriteTo(writer, property.ItemCode);
            ByteWriter.WriteTo(writer, property.IsBinding);
            ByteWriter.WriteTo(writer, property.IsDeal);
            WriteEquipmentProperty(writer, property.Property);

        }

        void WriteEquipmentProperty(BinaryWriter writer, EquipmentProperty property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            int num1 = (property.PropertyPluses == null) ? 0 : property.PropertyPluses.Count;
            ByteWriter.WriteTo(writer, num1);
            if (num1 > 0)
            {
                for (int i = 0; i < property.PropertyPluses.Count; i++)
                {
                    WriteEquipmentPropertyPlus(writer, property.PropertyPluses[i]);
                }
            }
        }

        void WriteEquipmentPropertyPlus(BinaryWriter writer, PropertyPlusEntity property)
        {
            ByteWriter.WriteTo(writer, property != null);
            if (property == null)
                return;
            ByteWriter.WriteTo(writer, property.PlusType);
            ByteWriter.WriteTo(writer, property.PlusValue);
            ByteWriter.WriteTo(writer, property.PropertyId);
        }
        #endregion

        #region 潘多拉

        /// <summary>
        /// 强化球员卡参数
        /// </summary>
        /// <returns></returns>
        void StrengthenParam()
        {
            Guid itemId1 = GetParamGuid("i1");
            if (!CheckParam(itemId1))
            {
                return;
            }
            Guid itemId2 = GetParamGuid("i2");
            if (!CheckParam(itemId2))
            {
                return;
            }
            var response = reader.StrengthenParam(UserAccount.ManagerId, itemId1,itemId2);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 强化球员卡
        /// </summary>
        /// <returns></returns>
        void Strengthen()
        {
            Guid itemId1 = GetParamGuid("i1");
            if (!CheckParam(itemId1))
            {
                return;
            }
            Guid itemId2 = GetParamGuid("i2");
            if (!CheckParam(itemId2))
            {
                return;
            }
            bool isProtect = GetParamBool("ip");
            Guid protectId = GetParamGuid("pi");
            var response = reader.Strengthen(UserAccount.ManagerId, itemId1, itemId2, isProtect, protectId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 分解球员卡
        /// </summary>
        /// <returns></returns>
        void Decompose()
        {
            var itemIds = GetParam("is");
            var response = reader.Decompose(UserAccount.ManagerId,itemIds);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 合成球员卡
        /// </summary>
        /// <returns></returns>
        void SynthesisNew()
        {
            var itemId1 = GetParamGuid("itemId1");
            if (!CheckParam(itemId1))
                return;
            var itemId2 = GetParamGuid("itemId2");
            if (!CheckParam(itemId2))
                return;
            var itemId3 = GetParamGuid("itemId3");
            if (!CheckParam(itemId3))
                return;
            var itemId4 = GetParamGuid("itemId4");
            if (!CheckParam(itemId4))
                return;
            var itemId5 = GetParamGuid("itemId5");
            if (!CheckParam(itemId5))
                return;
            var goldId = GetParamGuid("gi");
            var isProtect = GetParamBool("isProtect");
            var response = reader.SynthesisNew(UserAccount.ManagerId, itemId1, itemId2, itemId3, itemId4, itemId5, isProtect, LuckyId, goldId, HasTask);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 合成球员卡参数
        /// </summary>
        /// <returns></returns>
        void SynthesisParamNew()
        {
            var itemId1 = GetParamGuid("itemId1");
            if (!CheckParam(itemId1))
                return;
            var itemId2 = GetParamGuid("itemId2");
            if (!CheckParam(itemId2))
                return;
            var itemId3 = GetParamGuid("itemId3");
            if (!CheckParam(itemId3))
                return;
            var itemId4 = GetParamGuid("itemId4");
            if (!CheckParam(itemId4))
                return;
            var itemId5 = GetParamGuid("itemId5");
            if (!CheckParam(itemId5))
                return;
            bool isgold = GetParamBool("ig");
            var response = reader.SynthesisParamNew(UserAccount.ManagerId, itemId1, itemId2, itemId3, itemId4, itemId5, isgold);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 合同页合成参数
        /// </summary>
        /// <returns></returns>
        void TheContractSyntheticParam()
        {
            var itemId1 = GetParamGuid("i1");
            var itemId2 = GetParamGuid("i2");
            var itemId3 = GetParamGuid("i3");
            var itemId4 = GetParamGuid("i4");
            var itemId5 = GetParamGuid("i5");
            if (!CheckParam(itemId1) || !CheckParam(itemId2) || !CheckParam(itemId3) || !CheckParam(itemId4) || !CheckParam(itemId5))
            {
                return;
            }
            var response = reader.TheContractSyntheticParam(UserAccount.ManagerId, itemId1, itemId2, itemId3, itemId4, itemId5);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 合同页合成
        /// </summary>
        /// <returns></returns>
        void TheContractSynthetic()
        {
            var itemId1 = GetParamGuid("i1");
            var itemId2 = GetParamGuid("i2");
            var itemId3 = GetParamGuid("i3");
            var itemId4 = GetParamGuid("i4");
            var itemId5 = GetParamGuid("i5");
            if (!CheckParam(itemId1) || !CheckParam(itemId2) || !CheckParam(itemId3) || !CheckParam(itemId4) || !CheckParam(itemId5))
            {
                return;
            }
            var response = reader.TheContractSynthetic(UserAccount.ManagerId, itemId1, itemId2, itemId3, itemId4, itemId5);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 合成装备参数
        /// </summary>
        /// <returns></returns>
        void EquipmentSynthesisParam()
        {
            var itemId1 = GetParamGuid("i1");
            var itemId2 = GetParamGuid("i2");
            var itemId3 = GetParamGuid("i3");
            var itemId4 = GetParamGuid("i4");
            var itemId5 = GetParamGuid("i5");
            if (!CheckParam(itemId1) || !CheckParam(itemId2) || !CheckParam(itemId3) || !CheckParam(itemId4) || !CheckParam(itemId5))
            {
                return;
            }
           var resposne = reader.EquipmentSynthesisParam(UserAccount.ManagerId,  itemId1, itemId2, itemId3, itemId4, itemId5);
            OutputHelper.Output(resposne);
        }

        /// <summary>
        /// 装备合成
        /// </summary>
        /// <returns></returns>
        void EquipmentSynthesis()
        {
            var itemId1 = GetParamGuid("i1");
            var itemId2 = GetParamGuid("i2");
            var itemId3 = GetParamGuid("i3");
            var itemId4 = GetParamGuid("i4");
            var itemId5 = GetParamGuid("i5");
            var protectId = GetParamGuid("pi");
            var isProtect = GetParamBool("ip");
            if (!CheckParam(itemId1) || !CheckParam(itemId2) || !CheckParam(itemId3) || !CheckParam(itemId4) || !CheckParam(itemId5))
            {
                return;
            }
            var response = reader.EquipmentSynthesis(UserAccount.ManagerId, itemId1, itemId2, itemId3, itemId4, itemId5, isProtect, protectId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 装备出售
        /// </summary>
        /// <returns></returns>
        void EquipmentSell()
        {
            Guid itemId = GetParamGuid("id");
            var response = reader.EquipmentSell(UserAccount.ManagerId, itemId);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 装备出售
        /// </summary>
        /// <returns></returns>
        void PrpoSell()
        {
            var items = GetParam("ids");
            var response = reader.PrpoSell(UserAccount.ManagerId, items);
            OutputHelper.Output(response);
        }

        private void PlayerUpgradeTheStarParam()
        {
            var item = GetParamGuid("id");
            var response = reader.PlayerUpgradeTheStarParam(UserAccount.ManagerId, item);
            OutputHelper.Output(response);
        }

        private void PlayerUpgradeTheStar()
        {
            var item = GetParamGuid("id");
            var response = reader.PlayerUpgradeTheStar(UserAccount.ManagerId, item);
            OutputHelper.Output(response);
        }

        private void ResetPotentialParam()
        {
            var item = GetParamGuid("id");
            var lockstring = GetParam("ls");
            var response = reader.ResetPotentialParam(UserAccount.ManagerId, item, lockstring);
            OutputHelper.Output(response);
        }

        private void ResetPotential()
        {
            var item = GetParamGuid("id");
            var lockstring = GetParam("ls");
            var response = reader.ResetPotential(UserAccount.ManagerId, item, lockstring);
            OutputHelper.Output(response);
        }

        /// <summary>
        /// 兑换码礼包
        /// </summary>
        void Exchange()
        {
            var exchangeId = GetParam("ei");
            var pf = GetParam("pf");
            var response = reader.Exchange(UserAccount.ManagerId, exchangeId,pf);
            OutputHelper.Output(response);
        }

        #endregion

    }
}