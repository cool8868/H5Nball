using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Core.SkillCard
{
    public class SkillCardConfig
    {
        public static readonly int SKILLCardMaxBagSize = 240;//技能背包上限
        public static readonly int SKILLCardMaxBagMapLen = 10000;//技能背包上限
        public static readonly int SKILLCardMaxBenchSize = 16;//临时技能背包上限
        public static readonly int SKILLCardMaxCardLevel = 40;//技能卡最大等级
        public static readonly int SKILLCardMaxCardExp = 1000999999;//技能卡最大经验


        public static readonly int SKILLCardAskSkipRank = 4;//VIP学习级别
        public static readonly int SKILLCardMAXSkillCellSize = 7;//技能装备上限
        public static readonly int SKILLCardRAWSkillCellSize = 7;//初始技能可装备数

        public static readonly int SKILLCardMixVipLevel = 2;//一键合并所需vip等级
        public static readonly int SKILLCardAskVipLevel = 1;//一键学习所需vip等级
        public static readonly int SKILLCardAskSkipVipLevel = 1;//跳级学习所需vip等级
        public static readonly int SKILLCardPickVipLevel = 1;//一键拾取所需vip等级
        /// <summary>
        /// 技能升级配置
        /// </summary>
        private static Dictionary<int, ConfigSkillupgradeEntity> _SkillUpgradeDic;

        static SkillCardConfig()
        {
            try
            {
                SKILLCardMaxBagSize = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardMaxBagSize, SKILLCardMaxBagSize);
                SKILLCardMaxBagMapLen = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardMaxBagMapLen, SKILLCardMaxBagMapLen);
                SKILLCardRAWSkillCellSize = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardRAWSkillCellSize, SKILLCardRAWSkillCellSize);

                SKILLCardMixVipLevel = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardMixVipLevel, SKILLCardMixVipLevel);
                SKILLCardAskVipLevel = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardAskVipLevel, SKILLCardAskVipLevel);
                SKILLCardAskSkipVipLevel = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardAskSkipVipLevel, SKILLCardAskSkipVipLevel);
                SKILLCardPickVipLevel = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.SKILLCardPickVipLevel, SKILLCardPickVipLevel);
                _SkillUpgradeDic = new Dictionary<int, ConfigSkillupgradeEntity>();
                var allconfig = ConfigSkillupgradeMgr.GetAll();
                foreach (var item in allconfig)
                {
                    var key = GetKey(item.SkillLevel, item.Quality);
                    if (!_SkillUpgradeDic.ContainsKey(key))
                        _SkillUpgradeDic.Add(key, item);
                    else
                        _SkillUpgradeDic[key] = item;
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SkillCardConfig:Init", ex);
            }
        }

        private static int GetKey(int skillLevel, int quality)
        {
            return quality * 10000 + skillLevel;
        }

        /// <summary>
        /// 获取技能升级配置
        /// </summary>
        /// <param name="skillLevel"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static ConfigSkillupgradeEntity GetSkillUpgrade(int skillLevel, int quality)
        {
            var key = GetKey(skillLevel, quality);
            if (_SkillUpgradeDic.ContainsKey(key))
                return _SkillUpgradeDic[key];
            return null;
        }

    }
}
