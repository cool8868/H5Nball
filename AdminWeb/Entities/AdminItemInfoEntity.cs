using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.AdminWeb.Entities
{
    public class AdminItemInfoEntity
    {
        ///<summary>
        ///ItemId
        ///</summary>
        public System.Guid ItemId { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        public System.Int32 ItemCode { get; set; }

        public string Name { get; set; }

        ///<summary>
        ///物品类型
        ///</summary>
        public System.Int32 ItemType { get; set; }

        public string ItemTypeV { get; set; }

        ///<summary>
        ///二级分类(球员卡>颜色；装备>品质；)
        ///</summary>
        public System.Int32 SubType { get; set; }

        public string SubTypeV { get; set; }

        ///<summary>
        ///三级分类（装备>套装or散装）
        ///</summary>
        public System.Int32 ThirdType { get; set; }

        public string ThirdTypeV { get; set; }

        public int FourthType { get; set; }

        public string FourthTypeV { get; set; }

        ///<summary>
        ///强化级别
        ///</summary>
        public System.Int32 Strength { get; set; }

        ///<summary>
        ///堆叠数量
        ///</summary>
        public System.Int32 ItemCount { get; set; }

        ///<summary>
        ///是否绑定
        ///</summary>
        public System.Boolean IsBinding { get; set; }
        public bool IsBindingV { get; set; }

        /// <summary>
        /// 物品属性
        /// </summary>
        public ItemProperty ItemProperty { get; set; }

        public bool IsSelect { get; set; }

        /////<summary>
        /////物品属性
        /////</summary>
        //public EquipmentProperty EquipmentProperty { get; set; }


        /////<summary>
        /////物品属性
        /////</summary>
        //public PlayerCardProperty PlayerCardProperty { get; set; }

        /////<summary>
        /////物品属性
        /////</summary>
        //public MallItemProperty MallItemProperty { get; set; }

        /////<summary>
        /////物品属性
        /////</summary>
        //public BallSoulProperty BallSoulProperty { get; set; }

        ///<summary>
        ///所属格数
        ///</summary>
        public System.Int32 GridIndex { get; set; }

        ///<summary>
        ///状态
        ///</summary>
        public System.Int32 Status { get; set; }
        public string StatusV { get; set; }

        public bool IsRepeat { get; set; }
    }
}