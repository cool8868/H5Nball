using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.SkillCard
{
    #region Item
    [DataContract]
    [Serializable]
    public class DTOSkillCardItem : IFlatSplit
    {
        /// <summary>
        /// 技能卡标识Id
        /// </summary>
        [DataMember]
        public string ItemId
        {
            get;
            set;
        }
        /// <summary>
        /// 技能Code
        /// </summary>
        [DataMember]
        public string ItemCode
        {
            get;
            set;
        }
        /// <summary>
        /// 技能卡经验
        /// </summary>
        [DataMember]
        public int Exp
        {
            get;
            set;
        }
        /// <summary>
        /// 绑定标记
        /// </summary>
        [DataMember]
        public byte BindFlag
        {
            get;
            set;
        }
        public DicSkillcardEntity Cfg;
        /// <summary>
        /// 技能类型:1-射门;2-过人;3-防守;4-组织;5-守门
        /// </summary>
        [DataMember]
        public int SkillType
        {
            get;
            set;
        }
        /// <summary>
        /// 技能卡品质:1-绿;2-蓝;3-紫;4-橙
        /// </summary>
        [DataMember]
        public int SkillClass
        {
            get;
            set;
        }
        /// <summary>
        /// 技能等级
        /// </summary>
        [DataMember]
        public int SkillLevel
        {
            get;
            set;
        }

        #region IFlatSplit
        const int LENFlatSpit = 3;
        const int LENItemIdV1 = 22;
        public virtual string[] GetFlatSplit()
        {
            return new string[LENFlatSpit];
        }
        public virtual bool FromFlatSplit(string[] units)
        {
            if (null == units || units.Length < LENFlatSpit)
                return false;
            int cint = 0;
            this.ItemId = units[0];
            this.ItemCode = units[1];
            int.TryParse(units[2], out cint);
            this.Exp = cint;
            //int.TryParse(units[3], out cint);
            //this.BindFlag = (byte)cint;
            this.ConvertTo(1);
            return true;
        }
        public virtual bool ToFlatSplit(string[] units)
        {
            if (null == units || units.Length < LENFlatSpit)
                return false;
            units[0] = this.ItemId;
            units[1] = this.ItemCode;
            units[2] = this.Exp.ToString();
            //units[3] = this.BindFlag.ToString();
            return true;
        }
        protected void ConvertTo(int versionNo)
        {
            int lenItemId = this.ItemId.Length;
            if (lenItemId == LENItemIdV1)
                return;
            if (lenItemId > LENItemIdV1)
                this.ItemId = this.ItemId.Substring(0, LENItemIdV1);
        }
        #endregion
    }

    [DataContract]
    [Serializable]
    public class DTOSkillSetItem : DTOSkillCardItem
    {
        /// <summary>
        /// 装备栏位,=0未装备;1~11场上位置
        /// </summary>
        [DataMember]
        public int Index
        {
            get;
            set;
        }

        #region IFlatSplit
        const int LENFlatSpit = 4;
        public override string[] GetFlatSplit()
        {
            return new string[LENFlatSpit];
        }
        public override bool FromFlatSplit(string[] units)
        {
            if (null == units || units.Length < 3)
                return false;
            int cint = 0;
            this.ItemId = units[0];
            this.ItemCode = units[1];
            int.TryParse(units[2], out cint);
            this.Exp = cint;
            if (units.Length >= 4)
            {
                int.TryParse(units[3], out cint);
                this.Index = cint;
            }
            this.ConvertTo(1);
            return true;
        }
        public override bool ToFlatSplit(string[] units)
        {
            if (null == units || units.Length < 3)
                return false;
            units[0] = this.ItemId;
            units[1] = this.ItemCode;
            units[2] = this.Exp.ToString();
            if (units.Length >= 4)
                units[3] = this.Index.ToString();
            return true;
        }
        #endregion
    }
    #endregion

    #region View
    [DataContract]
    [Serializable]
    [KnownType(typeof(DTOSkillSetItem))]
    public class DTOSkillBagView
    {
        /// <summary>
        /// 技能卡列表
        /// </summary>
        [DataMember]
        public List<DTOSkillCardItem> CardList
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    [KnownType(typeof(DTOSkillSetItem))]
    public class DTOSkillBagViewV2
    {
        /// <summary>
        /// 技能卡列表
        /// </summary>
        [DataMember]
        public List<DTOSkillCardItem> CardList
        {
            get;
            set;
        }
        /// <summary>
        /// 背包道具列表
        /// </summary>
        [DataMember]
        public List<ItemInfoEntity> PackList
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
    public class DTOSkillAskView
    {
        /// <summary>
        /// 学习到的新技能，如不为空需弹出提示
        /// </summary>
        [DataMember]
        public DTOSkillSetItem CardNew
        {
            get;
            set;
        }
        /// <summary>
        /// 学习级别列表，元素>0代表级别开放
        /// </summary>
        [DataMember]
        public int[] AskList
        {
            get;
            set;
        }
        /// <summary>
        /// 学习记录列表，依次记录一键学习点亮的教练
        /// </summary>
        [DataMember]
        public int[] AskLog
        {
            get;
            set;
        }
        /// <summary>
        /// 技能卡列表，手动学习时为获取的新技能卡;自动学习时为临时背包所有
        /// </summary>
        [DataMember]
        public List<DTOSkillCardItem> CardList
        {
            get;
            set;
        }
        /// <summary>
        /// 经理同步属性信息
        /// </summary>
        [DataMember]
        public List<DTOWorthItem> WorthList
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
    public class DTOSkillSetView
    {
        /// <summary>
        /// 最大可装备技能数
        /// </summary>
        [DataMember]
        public int MaxSetCells
        {
            get;
            set;
        }
        /// <summary>
        /// 当前已装备技能数
        /// </summary>
        [DataMember]
        public int CntSetCells
        {
            get;
            set;
        }

        /// <summary>
        /// 当前阵型Id
        /// </summary>
        [DataMember]
        public int FormId
        {
            get;
            set;
        }
        /// <summary>
        /// 球员Id串
        /// </summary>
        [DataMember]
        public string PidStr
        {
            get;
            set;
        }
        /// <summary>
        /// 装备的技能列表
        /// </summary>
        [DataMember]
        public List<DTOSkillSetItem> SetList
        {
            get;
            set;
        }
       
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }

        /// <summary>
        ///剩余金币
        /// </summary>
        [DataMember]
        public int Coin { get; set; }
    }
    #endregion

}
