using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Core.ManagerSkill
{
    public class ManagerSkillConfig
    {
        public static readonly int MSKILLGoldItem4ResetTalent = 59002;//重置天赋商品id
        public static readonly int MSKILLPriceE4ResetTalent = 20;//重置天赋价格


        static ManagerSkillConfig()
        {
            try
            {
                MSKILLGoldItem4ResetTalent = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.MSKILLGoldItem4ResetTalent, MSKILLGoldItem4ResetTalent);
                MSKILLPriceE4ResetTalent = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.MSKILLPriceE4ResetTalent, MSKILLPriceE4ResetTalent);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ManagerSkillConfig:Init", ex);
            }
        }
    }
}
