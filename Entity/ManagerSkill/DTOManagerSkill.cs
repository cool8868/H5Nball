using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;

namespace Games.NBall.Entity.ManagerSkill
{
    #region Item
    [DataContract]
    [Serializable]
    public class DTOWillItem : IFlatSplit
    {
        /// <summary>
        /// 意志Code
        /// </summary>
        [DataMember]
        public string WillCode
        {
            get;
            set;
        }
        /// <summary>
        /// 状态标记:0-未收集完成;1-已收集完成;2-已启用
        /// </summary>
        [DataMember]
        public int EnableState
        {
            get;
            set;
        }
        /// <summary>
        /// 背包提示标记
        /// </summary>
        [DataMember]
        public bool HintFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 意志等级信息
        /// </summary>
        [DataMember]
        public DTOWillLevelItem LevelInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 已收集球员卡列表
        /// </summary>
        [DataMember]
        public List<DTOWillPartItem> PartList
        {
            get;
            set;
        }

        #region IFlatSplit
        const int LENFlatSpit = 3;
        public string[] GetFlatSplit()
        {
            return new string[LENFlatSpit];
        }
        public bool FromFlatSplit(string[] units)
        {
            if (null == units || units.Length < LENFlatSpit)
                return false;
            int cint = 0;
            this.WillCode = units[0];
            if (null == this.LevelInfo)
                this.LevelInfo = new DTOWillLevelItem();
            int.TryParse(units[1], out cint);
            this.LevelInfo.Lv = cint;
            int.TryParse(units[2], out cint);
            this.LevelInfo.Exp = cint;
            return true;
        }
        public bool ToFlatSplit(string[] units)
        {
            if (null == units || units.Length < LENFlatSpit)
                return false;
            units[0] = this.WillCode;
            if (null == this.LevelInfo)
            {
                units[1] = "1";
                units[2] = "0";
            }
            else
            {
                units[1] = this.LevelInfo.Lv.ToString();
                units[2] = this.LevelInfo.Exp.ToString();
            }
            return true;
        }
        #endregion
    }
    [DataContract]
    [Serializable]
    public class DTOWillLevelItem
    {
        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        public int Lv
        {
            get;
            set;
        }

        /// <summary>
        /// 当前经验值
        /// </summary>
        [DataMember]
        public int Exp
        {
            get;
            set;
        }

        /// <summary>
        /// 升级经验值
        /// </summary>
        [DataMember]
        public int MaxExp
        {
            get;
            set;
        }

        /// <summary>
        /// 当前效果参数1
        /// </summary>
        [DataMember]
        public double BuffArg
        {
            get;
            set;
        }

        /// <summary>
        /// 当前效果参数2
        /// </summary>
        [DataMember]
        public double BuffArg2
        {
            get;
            set;
        }

        /// <summary>
        /// 下一级效果参数1
        /// </summary>
        [DataMember]
        public double NextBuffArg
        {
            get;
            set;
        }

        /// <summary>
        /// 下一级效果参数2
        /// </summary>
        [DataMember]
        public double NextBuffArg2
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    public class DTOWillPartItem : IFlatSplit
    {
        /// <summary>
        /// 球员卡Id
        /// </summary>
        public string ItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 物品Code
        /// </summary>
        [DataMember]
        public int ItemCode
        {
            get;
            set;
        }

        /// <summary>
        /// 球员卡强化等级:=0表示背包有卡;>0表示已放入卡的强化等级
        /// </summary>
        [DataMember]
        public int PutStrength
        {
            get;
            set;
        }

        #region IFlatSplit
        const int LENFlatSpit = 3;
        public string[] GetFlatSplit()
        {
            return new string[LENFlatSpit];
        }
        public bool FromFlatSplit(string[] units)
        {
            if (null == units || units.Length < LENFlatSpit)
                return false;
            int cint = 0;
            this.ItemId = units[0];
            int.TryParse(units[1], out cint);
            this.ItemCode = cint;
            int.TryParse(units[2], out cint);
            this.PutStrength = cint;
            return true;
        }
        public bool ToFlatSplit(string[] units)
        {
            if (null == units || units.Length < LENFlatSpit)
                return false;
            units[0] = this.ItemId;
            units[1] = this.ItemCode.ToString();
            units[2] = this.PutStrength.ToString();
            return true;
        }
        #endregion

    }
    #endregion

    #region Rep
    [DataContract]
    [Serializable]
    public class DTOTalentView
    {
        /// <summary>
        /// 经理等级，点券信息
        /// </summary>
        [DataMember]
        public List<DTOWorthItem> WorthList
        {
            get;
            set;
        }

        /// <summary>
        /// 天赋点上限
        /// </summary>
        [DataMember]
        public int MaxTalentPoint
        {
            get;
            set;
        }

        /// <summary>
        /// 已用天赋点
        /// </summary>
        [DataMember]
        public int CntTalentPoint
        {
            get;
            set;
        }

        /// <summary>
        /// 被动天赋列表
        /// </summary>
        [DataMember]
        public List<string> NodoTalents
        {
            get;
            set;
        }
        /// <summary>
        /// 主动天赋列表
        /// </summary>
        [DataMember]
        public List<string> TodoTalents
        {
            get;
            set;
        }

        /// <summary>
        /// 装备的天赋
        /// </summary>
        [DataMember]
        public string[] SetTalents
        {
            get;
            set;
        }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }

    [DataContract]
    [Serializable]
    public class DTOWillView
    {
        /// <summary>
        /// 高级意志列表
        /// </summary>
        [DataMember]
        public List<DTOWillItem> HighWills
        {
            get;
            set;
        }

        /// <summary>
        /// 低级意志列表
        /// </summary>
        [DataMember]
        public List<DTOWillItem> LowWills
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    public class DTOWillItemView : DTOWillItem
    {

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
    #endregion


}
