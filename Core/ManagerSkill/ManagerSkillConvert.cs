using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.ManagerSkill;

namespace Games.NBall.Core.ManagerSkill
{
    public static class ManagerSkillExtension
    {
        /// <summary>
        /// 是否是主动技能
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool TodoFlag(this DicManagertalentEntity src)
        {
            return src.DriveFlag != (int)EnumSkillDriveType.Passive;
        }
        public static bool HighFlag(this DicManagerwillEntity src)
        {
            return src.WillRank > 1;
        }
    }
    public class ManagerSkillConvert
    {

        #region Get
        public static ManagerWillSrcWrap GetWillSrcWrap(Guid managerId, string willCode)
        {
            var src = ManagerskillWillsrcMgr.GetWillSrc(managerId, willCode);
            if (null == src)
            {
                src = new ManagerskillWillsrcEntity()
                {
                    ManagerId = managerId,
                    SkillCode = willCode,
                    EnableFlag = 0,
                    PartMap = string.Empty,
                };
            }
            return new ManagerWillSrcWrap(src);
        }
        #endregion

        #region Convert
        public static DTOTalentView ConvertToTalentView(ManagerSkillLibWrap lib, ManagerSkillUseWrap use)
        {
            var data = new DTOTalentView();
            data.MaxTalentPoint = lib.Raw.MaxTalentPoint;
            data.CntTalentPoint = lib.NodoTalents.Count + lib.TodoTalents.Count;
            data.NodoTalents = lib.NodoTalents.Keys.ToList();
            data.TodoTalents = lib.TodoTalents.Keys.ToList();
            if (null != use)
                data.SetTalents = use.SetTalents;
            return data;
        }
        #endregion

    }

    #region WillSrc
    public class ManagerWillSrcWrap
    {
        #region Cache
        readonly ManagerskillWillsrcEntity _raw;
        Dictionary<int, DTOWillPartItem> _partDic;
        #endregion

        #region .ctor
        public ManagerWillSrcWrap(ManagerskillWillsrcEntity raw)
        {
            this._raw = raw;
            this._raw.PartMap = this._raw.PartMap ?? string.Empty;
        }
        #endregion

        #region Facade
        public ManagerskillWillsrcEntity Raw
        {
            get { return this._raw; }
        }
        public Dictionary<int, DTOWillPartItem> PartList
        {
            get
            {
                if (null == this._partDic)
                {
                    var list = FlatTextFormatter.ListFromText<DTOWillPartItem>(_raw.PartMap);
                    var dic = new Dictionary<int, DTOWillPartItem>(list.Count);
                    list.ForEach(i => dic[i.ItemCode] = i);
                    this._partDic = dic;
                }
                return this._partDic;
            }
        }
        public string PartListText
        {
            get
            {
                if (null == this._partDic)
                    return this._raw.PartMap;
                return FlatTextFormatter.ListToText(this._partDic.Values);
            }
        }
        #endregion
    }
    #endregion
}
