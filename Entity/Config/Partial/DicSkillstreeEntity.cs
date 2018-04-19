using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{

    public partial class DicSkillstreeEntity
    {
        private List<RequireCondition> _ConditionList;

        public List<RequireCondition> ConditionList
        {
            get
            {
                if (_ConditionList == null)
                {
                    _ConditionList = new List<RequireCondition>(); //解析前置条件的技能
                    string[] list = this.Condition.Split(new char[] {'|'});
                    foreach (string con in list)
                    {
                        if (!string.IsNullOrEmpty(con.Trim()))
                        {
                            _ConditionList.Add(new RequireCondition(con));
                        }
                    }
                }
                return _ConditionList;
            }
        }

        private Dictionary<int, DicSkillstreedesdicEntity> _Description =
            new Dictionary<int, DicSkillstreedesdicEntity>();


        public void AddDesc(DicSkillstreedesdicEntity desc)
        {
            if (!_Description.ContainsKey(desc.SkillLevel))
            {
                _Description.Add(desc.SkillLevel, desc);
            }
        }

        public DicSkillstreedesdicEntity GetExtraInfo(int skillLevel)
        {
            DicSkillstreedesdicEntity desc;
            if (_Description.TryGetValue(skillLevel, out desc))
            {
                return desc;
            }
            else
            {
                return null;
            }
        }

    }

    /// <summary>
    /// 前置技能条件
    /// </summary>
    public class RequireCondition
    {
        ///所需要的前置
        ///技能要求
        ///比如
        ///MS001,1|MS002,2
        ///就表示需要MS001技能1点，MS002技能2点

        public RequireCondition(string conditionString)
        {
            string[] list = conditionString.Trim().Split(new char[] {','});
            this.SkillCode = list[0];
            this.Point = Convert.ToInt32(list[1]);
        }

        /// <summary>
        /// 前置技能code
        /// </summary>
        public string SkillCode { get; private set; }

        /// <summary>
        /// 需要该前置技能的点数
        /// </summary>
        public int Point { get; private set; }
    }

    public partial class DicSkillstreeResponse
    {

    }
}

