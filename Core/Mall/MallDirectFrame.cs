using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Core
{
    public class MallDirectFrame : IDisposable
    {
        private ConfigMalldirectEntity _configMalldirectEntity;
        private string _account;
        private Guid _managerId;
        private bool _hasCheck = false;
        private int _remainPoint = -1;
        private int _costPoint = 0;
        #region .ctor
        public MallDirectFrame(Guid managerId, EnumConsumeSourceType consumeSourceType, int usedCount = 0)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager != null)
            {
                _managerId = managerId;
                _account = manager.Account;
                _configMalldirectEntity = CacheFactory.MallCache.GetDirectEntity(consumeSourceType, usedCount);
                if (_configMalldirectEntity == null)
                {
                    string msg = "can't find mall direct config,consumeSourceType:" + consumeSourceType + ",usedCount:" +
                                 usedCount;
                    LogHelper.Insert(msg, LogType.Info);
                    throw new Exception(msg);
                }
            }
        }

        public MallDirectFrame(Guid managerId, string account, EnumConsumeSourceType consumeSourceType, int usedCount = 0)
        {
            _managerId = managerId;
            _account = account;
            _configMalldirectEntity = CacheFactory.MallCache.GetDirectEntity(consumeSourceType, usedCount);
            if (_configMalldirectEntity == null)
            {
                string msg = "can't find mall direct config,consumeSourceType:" + consumeSourceType + ",usedCount:" +
                             usedCount;
                LogHelper.Insert(msg, LogType.Info);
                throw new Exception(msg);
            }
        }

        ~MallDirectFrame()
        {
            Dispose();
        }
        #endregion

        #region Facade
        public void Dispose()
        {
            PayUserEntity = null;
            _account = "";
        }

        public PayUserEntity PayUserEntity { get; set; }

        public int RemainPoint { get { return _remainPoint; } }

        public int CostPoint { get { return _costPoint; } }

        /// <summary>
        /// 检查点券数量
        /// </summary>
        /// <returns></returns>
        public MessageCode Check()
        {
            if (_configMalldirectEntity == null || _configMalldirectEntity.Point <= 0)
            {
                return MessageCode.MallItemBuyFail;
            }
            PayUserEntity = PayCore.Instance.GetPayUser(_account);
            _costPoint = _configMalldirectEntity.Point;
            _remainPoint = PayUserEntity.TotalPoint - _configMalldirectEntity.Point;
            if (_remainPoint < 0)
                return MessageCode.NbPointShortage;
            return MessageCode.Success;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public MessageCode Save(string orderId, DbTransaction transaction = null)
        {
            if (!_hasCheck)
            {
                var checkCode = Check();
                if (checkCode != MessageCode.Success)
                    return checkCode;
            }
            return PayCore.Instance.ConsumePointForGamble(_account, _managerId,
                                                          _configMalldirectEntity.ConsumeSourceType, orderId, _configMalldirectEntity.Point,
                                                          transaction);
        }
        #endregion

        #region encapsulation

        #endregion
    }
}
