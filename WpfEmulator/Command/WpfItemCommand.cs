using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Item;
using Games.NBall.Entity.Response;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Command
{
    public class WpfItemCommand
    {
        public static string _moduleName = "Item";

        public static ItemPackageDataResponse GetPackage()
        {
            return RequestHelper.Request<ItemPackageDataResponse>(_moduleName, "getpackage");
        }

        /// <summary>
        /// 整理背包
        /// </summary>
        /// <returns></returns>
        public static ItemPackageDataResponse Arrange()
        {
            return RequestHelper.Request<ItemPackageDataResponse>(_moduleName, "arrange");
        }

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static ItemPackageDataResponse DeleteItem(Guid itemId,int count)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("i",itemId);
            parameter.Add("c", count);
            return RequestHelper.Request<ItemPackageDataResponse>(_moduleName, "deleteitem", parameter);
        }

        /// <summary>
        /// 拆分物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static ItemPackageDataResponse SplitItem(Guid itemId, int count)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("i", itemId);
            parameter.Add("c", count);
            return RequestHelper.Request<ItemPackageDataResponse>(_moduleName, "st", parameter);
        }

        /// <summary>
        /// 球员卡强化参数
        /// </summary>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <returns></returns>
        public static StrengthParamResponse StrengthenParam(Guid itemId1, Guid itemId2)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("itemId1", itemId1);
            parameter.Add("itemId2", itemId2);
            return RequestHelper.Request<StrengthParamResponse>(_moduleName, "strengthenparam", parameter);
        }

        /// <summary>
        /// 球员卡强化
        /// </summary>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="isProtect"></param>
        /// <returns></returns>
        public static StrengthResponse Strengthen(Guid itemId1, Guid itemId2, bool isProtect)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("itemId1", itemId1);
            parameter.Add("itemId2", itemId2);
            parameter.Add("isProtect", isProtect);
            parameter.AddHasTask(false);
            return RequestHelper.Request<StrengthResponse>(_moduleName, "strengthen", parameter);
        }

        /// <summary>
        /// 球员卡合成参数
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public static SynthesisParamResponse SynthesisParam(int cardLevel)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("cardLevel", cardLevel);
            return RequestHelper.Request<SynthesisParamResponse>(_moduleName, "synthesisparam", parameter);
        }

        /// <summary>
        /// 球员卡合成
        /// </summary>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <param name="isProtect"></param>
        /// <returns></returns>
        public static SynthesisResponse Synthesis(Guid itemId1, Guid itemId2, Guid itemId3, Guid itemId4, Guid itemId5,
                                    bool isProtect)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("itemId1", itemId1);
            parameter.Add("itemId2", itemId2);
            parameter.Add("itemId3", itemId3);
            parameter.Add("itemId4", itemId4);
            parameter.Add("itemId5", itemId5);
            parameter.Add("isProtect", isProtect);
            parameter.AddHasTask(false);
            return RequestHelper.Request<SynthesisResponse>(_moduleName, "synthesis", parameter);
        }

        /// <summary>
        /// 球员卡分解
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static DecomposeResponse Decompose(string itemIds)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("is", itemIds);
            parameter.AddHasTask(false);
            return RequestHelper.Request<DecomposeResponse>(_moduleName, "decompose", parameter);
        }


        /// <summary>
        /// 装备合成参数
        /// </summary>
        /// <param name="quality"></param>
        /// <param name="suitdrawingCode"></param>
        /// <returns></returns>
        public static EquipmentSynthesisParamResponse EquipmentSynthesisParam(int quality, int suitdrawingCode)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("quality", quality);
            parameter.Add("suitdrawingCode", suitdrawingCode);
            return RequestHelper.Request<EquipmentSynthesisParamResponse>(_moduleName, "equipmentsynthesisparam", parameter);
        }

        /// <summary>
        /// 装备合成
        /// </summary>
        /// <param name="itemId1"></param>
        /// <param name="itemId2"></param>
        /// <param name="itemId3"></param>
        /// <param name="itemId4"></param>
        /// <param name="itemId5"></param>
        /// <param name="isProtect"></param>
        /// <param name="suitdrawingId"></param>
        /// <returns></returns>
        public static SynthesisResponse EquipmentSynthesis(Guid itemId1, Guid itemId2, Guid itemId3,
                                                    Guid itemId4,
                                                    Guid itemId5, bool isProtect, Guid suitdrawingId)
        {
            WpfRequestParameter parameter = new WpfRequestParameter();
            parameter.Add("itemId1", itemId1);
            parameter.Add("itemId2", itemId2);
            parameter.Add("itemId3", itemId3);
            parameter.Add("itemId4", itemId4);
            parameter.Add("itemId5", itemId5);
            parameter.Add("isProtect", isProtect);
            parameter.Add("suitdrawingId", suitdrawingId);
            parameter.AddHasTask(false);
            return RequestHelper.Request<SynthesisResponse>(_moduleName, "equipmentsynthesis", parameter);
        }
    }
}
