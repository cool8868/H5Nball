using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.SkillCard;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;

namespace Games.NBall.Core.SkillCard
{
    public static class SkillCardExtension
    {
        public static bool TopExpFlag(this DTOSkillCardItem src)
        {
            if (null == src)
                return false;
            return src.Exp >= SkillCardConfig.SKILLCardMaxCardExp;
        }
        public static bool TopLevelFlag(this DTOSkillCardItem src)
        {
            if (null == src || null == src.Cfg)
                return false;
            //return src.Cfg.SkillClass >= (int)EnumSkillCardClass.Orange && src.Cfg.SkillLevel >= SkillCardConfig.MAXSkillCardLevel;
            return src.Cfg.SkillLevel >= SkillCardConfig.SKILLCardMaxCardLevel;
        }
        public static int[] AskList(this NbManagerskillaskEntity src)
        {
            return new int[] { 1, src.Ask2, src.Ask3, src.Ask4, src.Ask5 };
        }
    }
    public static class SkillCardConvert
    {
        #region Fill
        public static bool FillSkillCardConfig(DTOSkillCardItem src, bool forceFlag)
        {
            if (null == src
                || !forceFlag && null != src.Cfg && src.ItemCode == src.Cfg.SkillCode)
                return true;
            if (!SkillCardCache.Instance().TryGetSkillCard(src.ItemCode, out src.Cfg))
                return false;
            src.SkillType = src.Cfg.ActType;
            src.SkillClass = src.Cfg.SkillClass;
            src.SkillLevel = src.Cfg.SkillLevel;
            return true;
        }
        #endregion

        #region Get
        public static SkillBagWrap GetSkillBagWrap(Guid managerId)
        {
            return new SkillBagWrap(GetSkillBagRaw(managerId));
        }
        public static NbManagerskillbagEntity GetSkillBagRaw(Guid managerId)
        {
            var bag = NbManagerskillbagMgr.GetById(managerId);
            if (null == bag)
            {
                bag = new NbManagerskillbagEntity
                {
                    ManagerId = managerId,
                    SetMap = string.Empty
                };
            }
            return bag;
        }
        
        public static DTOSkillCardItem GetNewSkillCard(DicSkillcardEntity cfg)
        {
            return new DTOSkillCardItem
            {
                ItemId = FrameUtil.GenChar22Id(),
                ItemCode = cfg.SkillCode,
                Exp = cfg.MixExp,
                BindFlag = 0,
                Cfg = cfg,
                SkillType = cfg.ActType,
                SkillClass = cfg.SkillClass,
                SkillLevel = cfg.SkillLevel,
            };
        }
        public static DTOSkillCardItem GetMallSkillCard(ItemInfoEntity src)
        {
            if (null == src || src.ItemType != (int)EnumItemType.MallItem)
                return null;
            var mallCfg = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(src.ItemCode);
            if (null == mallCfg || mallCfg.EffectType != (int)EnumMallEffectType.SkillCardExp)
                return null;
            var obj = new DTOSkillCardItem();
            obj.ItemId = src.ItemId.ToString("N");
            obj.ItemCode = mallCfg.MallCode.ToString();
            obj.Exp = mallCfg.EffectValue;
            return obj;
        }
        public static DTOSkillSetItem GetNewSkillCardOn(DicSkillcardEntity cfg)
        {
            return new DTOSkillSetItem
            {
                ItemId = FrameUtil.GenChar22Id(),
                ItemCode = cfg.SkillCode,
                Exp = cfg.MixExp,
                BindFlag = 0,
                Cfg = cfg,
                SkillType = cfg.ActType,
                SkillClass = cfg.SkillClass,
                SkillLevel = cfg.SkillLevel,
            };
        }

        #endregion
    }

    #region SkillBagWrap
    public class SkillBagWrap
    {
        #region Config
        public const char SPLITSect = FlatTextFormatter.SPLITSect;
        public const char SPLITUnit = FlatTextFormatter.SPLITUnit;
        #endregion

        #region Cache
        readonly NbManagerskillbagEntity _raw;
        Dictionary<string, byte> _setSkills;
        Dictionary<string, DTOSkillSetItem> _setList;
        #endregion

        #region .ctor
        public SkillBagWrap(NbManagerskillbagEntity bag)
        {
            this._raw = bag;
        }
        #endregion

        #region Facade
        public NbManagerskillbagEntity RawBag
        {
            get { return this._raw; }
        }
        public Dictionary<string, byte> SetSkills
        {
            get
            {
                if (null == this._setSkills)
                    this._setSkills = FrameConvert.SkillDicFromText(_raw.SetSkills, SPLITUnit);
                return this._setSkills;
            }
        }
        public string SetSkillsText
        {
            get
            {
                if (null == this._setSkills)
                    return this._raw.SetSkills;
                return string.Join(SPLITUnit.ToString(), this._setSkills.Keys.ToArray());
            }
        }
        public Dictionary<string, DTOSkillSetItem> SetList
        {
            get
            {
                if (null == this._setList)
                {
                    var list = FlatTextFormatter.ListFromText<DTOSkillSetItem>(this._raw.SetMap, SPLITSect, SPLITUnit);
                    var dic = new Dictionary<string, DTOSkillSetItem>(list.Count);
                    foreach (var item in list)
                    {
                        SkillCardConvert.FillSkillCardConfig(item, false);
                        if (null == item.Cfg)
                            dic[item.ItemCode] = item;
                        else
                            dic[item.Cfg.SkillRoot] = item;
                    }
                    this._setList = dic;
                }
                return this._setList;
            }
        }
        public string SetMap
        {
            get
            {
                if (null == this._setList)
                    return this._raw.SetMap;
                return FlatTextFormatter.ListToText(this._setList.Values, SPLITSect, SPLITUnit);
            }
        }
        public int CntSetNum
        {
            get
            {
                int val = 0;
                foreach (var item in this.SetList.Values)
                {
                    if (item.Index > 0)
                        ++val;
                }
                return val;
            }
        }
      
        public List<DTOSkillSetItem> GetShowSet()
        {
            return this.SetList.Values.OrderByDescending(i => i.SkillClass).ThenByDescending(i => i.ItemCode).ToList();
        }

        public string SetSkillsTextFromLib()
        {
            string[] skills = new string[SkillCardConfig.SKILLCardMAXSkillCellSize];
            for (int i = 0; i < skills.Length; ++i)
            {
                skills[i] = string.Empty;
            }
            var lib = this.SetList;
            foreach (var item in this.SetList.Values)
            {
                if (item.Index <= 0)
                    continue;
                skills[item.Index - 1] = item.ItemCode;
            }
            return string.Join(",", skills);
        }

        List<DTOSkillCardItem> GetShowList(ICollection<DTOSkillCardItem> rawList)
        {
            foreach (var item in rawList)
            {
                SkillCardConvert.FillSkillCardConfig(item, false);
            }
            return rawList.OrderByDescending(i => i.SkillClass).ThenByDescending(i => i.ItemCode).ThenByDescending(i => i.Exp).ToList();
        }
        #endregion
    }
    #endregion

}
