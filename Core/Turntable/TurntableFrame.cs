using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Core.Turntable
{
    public class TurntableFrame
    {
        /// <summary>
        /// 转盘详情
        /// </summary>
        private TurntableFrameEntity _turntableFrame { get; set; }
        /// <summary>
        /// 转盘经理对象
        /// </summary>
        private TurntableManagerEntity _turntableManagerEntity;

        /// <summary>
        /// 转盘重置次数字典
        /// </summary>
        private Dictionary<int, int> _turntableResetDic { get; set; }

        /// <summary>
        /// 所有转盘集合
        /// </summary>
        public Dictionary<int, TurntableList> TurnTableListDic { get; set; }

        /// <summary>
        /// 转盘项
        /// </summary>
        public TurntableList TurnTableDic { get; set; }

        public TurntableManagerEntity TurntableManagerEntity { get { return _turntableManagerEntity; } }

        private bool isUpdate = false;

        public TurntableFrame(Guid managerId)
        {
            var frame =TurntableManagerMgr.GetById(managerId);
            DateTime date = DateTime.Now;
            if (frame == null)
            {
                frame = new TurntableManagerEntity(managerId, 0, CacheFactory.TurntableCache.GiveLuckyCoin, CacheFactory.TurntableCache.DayProduceLuckyCoin, (int)EnumTurntableType.Bronze, new byte[0],"", date, date, date);
                TurntableManagerMgr.Insert(frame);
            }
            if (frame.RefreshDate != date.Date) 
            {
                if (TurntableCore.Instance.IsActivity)
                {
                    frame.GiveLuckyCoin = CacheFactory.TurntableCache.GiveLuckyCoin;
                    frame.DayProduceLuckyCoin = CacheFactory.TurntableCache.DayProduceLuckyCoin;
                    frame.ResetNumber = "1,0|2,0|3,0";
                    frame.RefreshDate = date.Date;
                    isUpdate = true;
                }
            }
            _turntableManagerEntity = frame;
            AnalyseTurntable();
        }

        /// <summary>
        /// 获取转盘信息
        /// </summary>
        /// <returns></returns>
        public List<TurntableItem> GetTurntableList() 
        {
            if (_turntableFrame.TurntableInfo == null || _turntableFrame.TurntableInfo.Count == 0) 
            {
                Initialization((int)EnumTurntableType.Bronze);
            }
            if (TurnTableDic.ItemList.Count > 0) 
            {
                return TurnTableDic.ItemList;
            }
            return new List<TurntableItem>();
        }

        /// <summary>
        /// 初始化转盘
        /// </summary>
        /// <param name="turntableType"></param>
        public void Initialization(int turntableType) 
        {
            var list = CacheFactory.TurntableCache.GetTurntableList(turntableType);
            if (list.Count == 0)
                return;
            TurnTableDic = new TurntableList();
            var turntable = new  List<TurntableItem>();
            foreach (var item in list)
            {
                TurntableItem entity = new TurntableItem();
                entity.Idx = item.TurntableId;
                entity.IsEffective = true;
                entity.Rate = item.InitialRate;
                entity.IsTurntable = false;
                if (item.PrizeType == (int)EnumTurntablePrizeType.Turntable)
                    entity.IsTurntable = true;
                else if (item.PrizeType == (int) EnumTurntablePrizeType.Special)
                    entity.SpecialItem = AnalysisSpecial(item);
                turntable.Add(entity);
            }
            TurnTableDic.ItemList = turntable;
            TurnTableDic.IsFirst = true;
            if (!TurnTableListDic.ContainsKey(turntableType))
                TurnTableListDic.Add(turntableType, new TurntableList());
            TurnTableListDic[turntableType] = TurnTableDic;
            _turntableManagerEntity.TurntableType = turntableType;
            isUpdate = true;
        }

        /// <summary>
        /// 转到转盘
        /// </summary>
        /// <param name="turntableType"></param>
        /// <param name="isFirst"></param>
        public void GoToTurntable(int turntableType, bool isFirst=false)
        {
            if (!TurnTableListDic.ContainsKey(turntableType))
            {
                if (turntableType < 1 || turntableType > 3)
                    return;
                Initialization(turntableType);
                return;
            }
            var turntable = TurnTableListDic[turntableType];
            if (isFirst)
                turntable.IsFirst = true;
            TurnTableDic = turntable;
            _turntableManagerEntity.TurntableType = turntableType;
            isUpdate = true;
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="specialItem">特殊物品code</param>
        /// <returns></returns>
        public ConfigTurntableprizeEntity LuckDraw(ref int specialItem) 
        {
            if (_turntableManagerEntity.GiveLuckyCoin > 0)
                _turntableManagerEntity.GiveLuckyCoin--;
            else if (_turntableManagerEntity.LuckyCoin > 0)
                _turntableManagerEntity.LuckyCoin--;
            ConfigTurntableprizeEntity resultPrize = null;
            int rateNumber = RandomHelper.GetInt32(0, 10000);
            int startRate = 0;//初始概率
            int distributionRate = 0;//抽奖后可分配概率
            int distributionItemNumber = 0;//剩余分配概率的项
            bool isWinAlottert = false;//是否抽奖完成
            var isFirst = false;
            if (TurnTableDic.IsFirst)
            {
                isFirst = true;
                TurnTableDic.IsFirst = false;
            }
            foreach (var item in TurnTableDic.ItemList)
            {
                if (!item.IsEffective)
                    continue;
                if (!isWinAlottert)
                {
                    startRate += item.Rate;
                    if (startRate >= rateNumber && rateNumber>= startRate-item.Rate)
                    {
                        resultPrize = CacheFactory.TurntableCache.GetTurntablePrize(_turntableManagerEntity.TurntableType, item.Idx);
                        if (resultPrize == null)
                        {
                            return null;
                        }
                        //抽到转盘 直接刷新
                        if (item.IsTurntable)
                        {
                            if (isFirst)//首次不能抽到转盘
                            {
                                resultPrize = null;
                                distributionItemNumber++;
                                startRate = startRate - item.Rate;
                                continue;
                            }
                            if (resultPrize.PrizeType == (int)EnumTurntablePrizeType.Turntable)
                            {
                                GoToTurntable(resultPrize.SubType,true);
                                return resultPrize;
                            }
                           
                        }
                        if (resultPrize.PrizeType == (int)EnumTurntablePrizeType.Special)
                            specialItem = item.SpecialItem;
                        item.IsEffective = false;
                        distributionRate = item.Rate;
                        item.Rate = 0;
                        isWinAlottert = true;
                        continue;
                    }
                }
                distributionItemNumber++;
            }
            //distributionItemNumber = distributionItemNumber - 2;//减去两个转盘，转盘概率不变
            if (distributionItemNumber - 2 <= 0) //自动重置转盘
            {
                Initialization(_turntableManagerEntity.TurntableType);
            }
            else
            {
                bool isadd1 = false;//是否能除净
                if (distributionRate % distributionItemNumber != 0)
                    isadd1 = true;
                int averageDistributionRate = distributionRate / distributionItemNumber;
                if (isadd1)
                    averageDistributionRate++;
                //重新计算概率
                foreach (var item in TurnTableDic.ItemList)
                {
                    //if (item.IsTurntable || !item.IsEffective)
                    if (!item.IsEffective)
                        continue;
                    item.Rate += averageDistributionRate;
                }
            }
            isUpdate = true;
            return resultPrize;
        }

        /// <summary>
        /// 获取重置需要消耗钻石
        /// </summary>
        /// <returns></returns>
        public int GetResetPoint()
        {
            var number = GetResetNumber(_turntableManagerEntity.TurntableType);
            var point = CacheFactory.TurntableCache.GetResetPoint(_turntableManagerEntity.TurntableType, number);
            return point;
        }

        /// <summary>
        /// 获取重置需要消耗钻石
        /// </summary>
        /// <param name="turntableType"></param>
        /// <returns></returns>
        public int GetResetPoint(int turntableType)
        {
            var number = GetResetNumber(turntableType);
            var point = CacheFactory.TurntableCache.GetResetPoint(turntableType, number);
            return point;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="turntableType"></param>
        public MessageCode Reset(int turntableType)
        {
            if (!_turntableResetDic.ContainsKey(turntableType))
                return MessageCode.NbParameterError;
            var value = _turntableResetDic[turntableType];
            value++;
            _turntableResetDic[turntableType] = value;
            Initialization(turntableType);
            string resetNumber = "";
            foreach (var i in _turntableResetDic)
            {
                resetNumber += i.Key + "," + i.Value + "|";
            }
            if (resetNumber.Length > 0)
                resetNumber = resetNumber.Substring(0, resetNumber.Length - 1);
            _turntableManagerEntity.ResetNumber = resetNumber;
            isUpdate = true;
            return MessageCode.Success;
        }

        /// <summary>
        /// 获取重置次数
        /// </summary>
        /// <param name="turntableType"></param>
        /// <returns></returns>
        public int GetResetNumber(int turntableType)
        {
            if (_turntableResetDic == null || _turntableResetDic.Count == 0)
            {
                _turntableResetDic = new Dictionary<int, int>();
                var resetString = _turntableManagerEntity.ResetNumber;//获取重置串
                if (resetString.Length == 0)//初始化
                    resetString = "1,0|2,0|3,0";
                var resetList = resetString.Split('|');
                if (resetList.Length < 3)
                {
                    resetString = "1,0|2,0|3,0";
                    resetList = resetString.Split('|');
                }
                foreach (var s in resetList)
                {
                    var itemList = s.Split(',');
                    var tType = ConvertHelper.ConvertToInt(itemList[0]);
                    var resetNumber = ConvertHelper.ConvertToInt(itemList[1]);
                    if (tType == 0)
                        return 0;
                    if (!_turntableResetDic.ContainsKey(tType))
                        _turntableResetDic.Add(tType, resetNumber);
                }
            }
            if (_turntableResetDic.ContainsKey(turntableType))
                return _turntableResetDic[turntableType];
            return 0;
        }

        /// <summary>
        /// 解析特殊物品
        /// </summary>
        /// <param name="prizeEntity"></param>
        /// <returns></returns>
        public int AnalysisSpecial(ConfigTurntableprizeEntity prizeEntity)
        {
            if (prizeEntity.SpecialString.Length <= 0)
                return 0;
            var prizeList = prizeEntity.SpecialString.Split(',');
            int index = RandomHelper.GetInt32WithoutMax(0, prizeList.Length);
            int prizeCode = ConvertHelper.ConvertToInt(prizeList[index]);
            if (prizeCode == 0)
                return 0;
            return prizeCode;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save(DbTransaction trans = null)
        {
            var turntableString = GenerateFightMapString();
            _turntableManagerEntity.DetailsString = turntableString;
            _turntableManagerEntity.UpdateTime = DateTime.Now;
            if (!TurntableManagerMgr.Update(_turntableManagerEntity, trans))
                return false;
            return true;
        }

        public bool Save(bool isCheckUpdate, DbTransaction trans = null)
        {
            if (isCheckUpdate)
            {
                if (!isUpdate)
                    return true;
            }
            return Save(trans);
        }

        #region 解析字符串
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void AnalyseTurntable()
        {
            _turntableFrame = SerializationHelper.FromByte<TurntableFrameEntity>(_turntableManagerEntity.DetailsString);
            if (_turntableFrame == null || _turntableFrame.TurntableInfo == null)
            {
                _turntableFrame = new TurntableFrameEntity();
                TurnTableListDic = new Dictionary<int, TurntableList>();
                Initialization((int)EnumTurntableType.Bronze);
            }
            else
            {
                TurnTableListDic = _turntableFrame.TurntableInfo;
                if (_turntableManagerEntity.TurntableType < 1 || _turntableManagerEntity.TurntableType > 3)
                    _turntableManagerEntity.TurntableType = 1;
                GoToTurntable(_turntableManagerEntity.TurntableType);
            }
        }
        #endregion
        /// <summary>
        /// 获取对阵字符串
        /// </summary>
        private byte[] GenerateFightMapString()
        {
            TurnTableListDic[_turntableManagerEntity.TurntableType] = TurnTableDic;
            _turntableFrame.TurntableInfo = TurnTableListDic;
            return SerializationHelper.ToByte(_turntableFrame);
        }
    }
}
