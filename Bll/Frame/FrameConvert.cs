using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.ManagerSkill;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Frame
{
    public class FrameConvert
    {
        #region WorthList
        public static List<DTOWorthItem> GetWorthList(Guid managerId, params EnumWorthType[] worthTypes)
        {
            var worthSrc = new WorthSource();
            return GetWorthList(managerId, worthSrc, worthTypes);
        }
        public static List<DTOWorthItem> GetWorthList(Guid managerId, WorthSource worthSrc, params EnumWorthType[] worthTypes)
        {
            var lst = new List<DTOWorthItem>();
            foreach (var worthType in worthTypes)
            {
                lst.Add(new DTOWorthItem
                {
                    WorthType = (int)worthType,
                    Value = GetWorthValue(managerId, worthSrc, worthType),
                    CostValue = 0,
                });
            }
            return lst;
        }
        public static long GetWorthValue(Guid managerId, EnumWorthType worthType)
        {
            var worthSrc = new WorthSource();
            return GetWorthValue(managerId, worthSrc, worthType);
        }
        public static long GetWorthValue(Guid managerId, WorthSource worthSrc, EnumWorthType worthType)
        {
            long val = -1;
            try
            {
                if (null == worthSrc)
                    worthSrc = new WorthSource();
                if (null == worthSrc.Manager)
                    worthSrc.Manager = NbManagerMgr.GetById(managerId);
                if (null == worthSrc.Manager)
                    return val;
                switch (worthType)
                {
                    case EnumWorthType.Gold:
                        if (null == worthSrc.Account)
                            worthSrc.Account = GetPayAccount(worthSrc.Manager.Account);
                        return worthSrc.Account.TotalPoint;
                    case EnumWorthType.Coin:
                        return worthSrc.Manager.Coin;
                    case EnumWorthType.ManagerLevel:
                        return worthSrc.Manager.Level;
                    case EnumWorthType.VipLevel:
                        return worthSrc.Manager.VipLevel;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("FrameConvert:GetWorthValue", ex);
            }
            return val;
        }
        public static WorthSource GetWorthSource(Guid managerId, bool managerFlag, bool accountFlag, string siteId = "")
        {
            var obj = new WorthSource();
            try
            {
                if (managerFlag || accountFlag)
                    obj.Manager = NbManagerMgr.GetById(managerId, siteId);
                if (accountFlag)
                    obj.Account = GetPayAccount(obj.Manager.Account, siteId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("FrameConvert:GetWorthSource", ex);
            }
            return obj;
        }
        public static MessageCode GetWorthErrorCode(EnumWorthType worthType)
        {
            switch (worthType)
            {
                case EnumWorthType.Gold:
                    return MessageCode.LackofGold;
                case EnumWorthType.Coin:
                    return MessageCode.LackofCoin;
                case EnumWorthType.ManagerLevel:
                    return MessageCode.LackofManagerLevel;
                case EnumWorthType.VipLevel:
                    return MessageCode.LackofVipLevel;
            }
            return MessageCode.NbParameterError;
        }
        static PayUserEntity GetPayAccount(string account, string siteId = "")
        {
            var pay = PayUserMgr.GetById(account, siteId);
            if (null == pay)
            {
                pay = new PayUserEntity();
                pay.Account = account;
                pay.RowTime = DateTime.Now;
            }
            return pay;

        }
        #endregion

        #region SkillList
        public static Dictionary<string, byte> SkillDicFromText(string text, char splitUnit = FlatTextFormatter.SPLITUnit)
        {
            if (string.IsNullOrEmpty(text))
                return new Dictionary<string, byte>();
            string[] list = text.Trim(splitUnit).Split(splitUnit);
            var dic = new Dictionary<string, byte>(list.Length);
            foreach (string val in list)
            {
                dic[val] = 0;
            }
            return dic;
        }
        public static List<string>[] SkillListFromText(string text, int cnt, char splitSect = FlatTextFormatter.SPLITSect, char splitUnit = FlatTextFormatter.SPLITUnit)
        {
            string[] sects = text.Trim(splitSect).Split(splitSect);
            var list = new List<string>[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                if (sects.Length > i)
                    list[i] = sects[i].Split(splitUnit).ToList();
            }
            return list;
        }
        public static string SkillListToText(List<string>[] list, char splitSect = FlatTextFormatter.SPLITSect, char splitUnit = FlatTextFormatter.SPLITUnit)
        {
            if (list == null)
                return "";
            int cnt = list.Length;
            string[] sects = new string[list.Length];
            for (int i = 0; i < cnt; ++i)
            {
                sects[i] = (null == list[i]) ? string.Empty : string.Join(splitUnit.ToString(), list[i]);
            }
            return string.Concat(splitSect.ToString(), string.Join(splitSect.ToString(), sects));
        }
        #endregion

        #region GenItem
        public static string GenItemsMap(GenItemProdu itemProdu)
        {
            if (null == itemProdu)
                return string.Empty;
            return itemProdu.ItemMap;
        }
        public static GenItemProdu GenItemProdu(IEnumerable<GenItemProto> protos)
        {
            if (null == protos)
                return null;
            var obj = new GenItemProdu();
            GenItems(obj, protos);
            return obj;
        }
        public static void GenItems(GenItemProdu produ, IEnumerable<GenItemProto> protos, bool clearFlag = false)
        {
            if (clearFlag)
                produ.Clear();
            foreach (var item in protos)
            {
                GenItem(produ, item);
            }
        }
        public static void GenItem(GenItemProdu produ, GenItemProto proto, int qty = 0)
        {
            if (null == produ || null == proto)
                return;
            if (qty <= 0)
                qty = proto.Qty;
            switch ((EnumItemGenType)proto.GenType)
            {
                case EnumItemGenType.Coin:
                    produ.Coin += qty;
                    return;
                case EnumItemGenType.Point:
                    produ.Point += qty;
                    return;
                case EnumItemGenType.Prestige:
                    produ.Prestige += qty;
                    return;
            }
            if (qty == 0)
                qty = 1;
            int itemCode = 0;
            for (int i = 0; i < qty; i++)
            {
                switch ((EnumItemGenType)proto.GenType)
                {
                    case EnumItemGenType.SpecItem:
                        itemCode = proto.ItemCode;
                        break;
                    case EnumItemGenType.RandItem:
                        break;
                    case EnumItemGenType.SpecRandItem:
                    case EnumItemGenType.LibRandItem:
                    case EnumItemGenType.MultiRandSuit:
                        int cnt = null == proto.ItemList ? 0 : proto.ItemList.Count;
                        if (cnt == 0)
                            return;
                        itemCode = cnt == 1 ? proto.ItemList[0] : proto.ItemList[RandomPicker.RandomInt(0, cnt - 1)];
                        break;
                }
                if (itemCode == 0)
                    return;
                if (proto.GenType == (int)EnumItemGenType.MultiRandSuit)
                {
                    var suits = LotteryCache.Instance.LotteryEquipmentSuitRange(itemCode, proto.ItemRank);
                    if (null == suits || suits.Count == 0)
                        return;
                    foreach (int code in suits)
                    {
                        produ.PackItems.Add(new LotteryEntity(code, code.ToString(), proto.Strength, proto.BindFlag > 0));
                    }
                    return;
                }
                if (proto.GenType == (int)EnumItemGenType.LibRandItem)
                {
                    itemCode = LotteryCache.Instance.LotteryByLib(itemCode);
                    if (itemCode == 0)
                        return;
                }
                if (proto.ItemType == (int)EnumItemType.MallItem)
                {
                    produ.PackItems.Add(new LotteryEntity(itemCode, itemCode.ToString(), proto.Strength, proto.BindFlag > 0, qty));
                    return;
                }
                produ.PackItems.Add(new LotteryEntity(itemCode, itemCode.ToString(), proto.Strength, proto.BindFlag > 0));
            }
        }
        #endregion
    }

    #region SkillUse
    public class ManagerSkillUseWrap
    {
        #region Cache
        readonly char _splitSect;
        readonly char _splitUnit;
        readonly ManagerskillUseEntity _raw;
        List<string>[] _managerSkills;
        string[] _setTalents;
        Dictionary<string, byte> _setWills;
        #endregion

        #region .ctor
        public ManagerSkillUseWrap(ManagerskillUseEntity raw)
        {
            this._splitSect = FlatTextFormatter.SPLITSect;
            this._splitUnit = FlatTextFormatter.SPLITUnit;
            this._raw = raw;
            this._raw.ManagerSkills = this._raw.ManagerSkills ?? string.Empty;
            this._raw.Talents = this._raw.Talents ?? string.Empty;
            this._raw.Wills = this._raw.Wills ?? string.Empty;
        }
        #endregion

        #region Facade
        public ManagerskillUseEntity Raw
        {
            get { return this._raw; }
        }
        public ulong VersionNo
        {
            get
            {
                if (null == this.Raw.RowVersion)
                    return 0;
                return BitConverter.ToUInt64(this.Raw.RowVersion, 0);
            }
        }
        public List<string>[] ManagerSkills
        {
            get
            {
                if (null == this._managerSkills)
                    this._managerSkills = FrameConvert.SkillListFromText(_raw.ManagerSkills, 3, _splitSect, _splitUnit);
                return this._managerSkills;
            }
        }
        public string ManagerSkillsText
        {
            get
            {
                if (null == this._managerSkills)
                    return this._raw.ManagerSkills;
                return FrameConvert.SkillListToText(this._managerSkills, _splitSect, _splitUnit);
            }
        }
        public string[] SetTalents
        {
            get
            {
                if (null == this._setTalents)
                {
                    string[] units = _raw.Talents.Split(_splitUnit);
                    var list = new string[2];
                    list[0] = units.Length > 0 ? units[0] : string.Empty;
                    list[1] = units.Length > 1 ? units[1] : string.Empty;
                    this._setTalents = list;
                }
                return this._setTalents;
            }
        }
        public string SetTalentsText
        {
            get
            {
                if (null == this._setTalents)
                    return this._raw.Talents;
                return string.Join(_splitUnit.ToString(), this._setTalents);
            }
        }
        public Dictionary<string, byte> SetWills
        {
            get
            {
                if (null == this._setWills)
                    this._setWills = FrameConvert.SkillDicFromText(_raw.Wills, _splitUnit);
                return this._setWills;
            }
        }
        public string SetWillsText
        {
            get
            {
                if (null == this._setWills)
                    return this._raw.Wills;
                return string.Join(_splitUnit.ToString(), this._setWills.Keys.ToArray());
            }
        }
        public int[] OnPids
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region SkillLib
    public class ManagerSkillLibWrap
    {
        #region Cache
        readonly char _splitSect;
        readonly char _splitUnit;
        readonly ManagerskillLibEntity _raw;
        Dictionary<string, byte> _nodoTalents;
        Dictionary<string, byte> _todoTalents;
        Dictionary<string, byte> _lowWills;
        Dictionary<string, DTOWillItem> _highWills;
        #endregion

        #region .ctor
        public ManagerSkillLibWrap(ManagerskillLibEntity raw)
        {
            this._splitSect = FlatTextFormatter.SPLITSect;
            this._splitUnit = FlatTextFormatter.SPLITUnit;
            this._raw = raw;
            this._raw.NodoTalents = this._raw.NodoTalents ?? string.Empty;
            this._raw.TodoTalents = this._raw.TodoTalents ?? string.Empty;
            this._raw.NodoWills = this._raw.NodoWills ?? string.Empty;
            this._raw.TodoWills = this._raw.TodoWills ?? string.Empty;
        }
        #endregion

        #region Facade
        public ManagerskillLibEntity Raw
        {
            get { return this._raw; }
        }
        public Dictionary<string, byte> NodoTalents
        {
            get
            {
                if (null == this._nodoTalents)
                    this._nodoTalents = FrameConvert.SkillDicFromText(_raw.NodoTalents, _splitUnit);
                return this._nodoTalents;
            }
        }
        public string NodoTalentsText
        {
            get
            {
                if (null == this._nodoTalents)
                    return this._raw.NodoTalents;
                return string.Join(_splitUnit.ToString(), this._nodoTalents.Keys.ToArray());
            }
        }
        public Dictionary<string, byte> TodoTalents
        {
            get
            {
                if (null == this._todoTalents)
                    this._todoTalents = FrameConvert.SkillDicFromText(_raw.TodoTalents, _splitUnit);
                return this._todoTalents;
            }
        }
        public string TodoTalentsText
        {
            get
            {
                if (null == this._todoTalents)
                    return this._raw.TodoTalents;
                return string.Join(_splitUnit.ToString(), this._todoTalents.Keys.ToArray());
            }
        }
        public Dictionary<string, byte> LowWills
        {
            get
            {
                if (null == this._lowWills)
                    this._lowWills = FrameConvert.SkillDicFromText(_raw.NodoWills, _splitUnit);
                return this._lowWills;
            }
        }
        public string LowWillsText
        {
            get
            {
                if (null == this._lowWills)
                    return this._raw.NodoWills;
                return string.Join(_splitUnit.ToString(), this._lowWills.Keys.ToArray());
            }
        }
        public Dictionary<string, DTOWillItem> HighWills
        {
            get
            {
                if (null == this._highWills)
                {
                    var list = FlatTextFormatter.ListFromText<DTOWillItem>(_raw.TodoWills);
                    var dic = new Dictionary<string, DTOWillItem>(list.Count);
                    list.ForEach(i => dic[i.WillCode] = i);
                    this._highWills = dic;
                }
                return this._highWills;
            }
        }
        public string HigthWillsText
        {
            get
            {
                if (null == this._highWills)
                    return this._raw.TodoWills;
                return FlatTextFormatter.ListToText<DTOWillItem>(this._highWills.Values);
            }
        }
        #endregion

    }
    #endregion
}
