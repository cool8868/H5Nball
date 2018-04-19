using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Core.Mall
{
    public class CrossMallDirectFrame:IDisposable
    {
        private ConfigMalldirectEntity _configMalldirectEntity;
        private string _account;
        private Guid _managerId;
        private bool _hasCheck=false;
        private int _remainPoint = -1;
        private string _siteId;
        #region .ctor
        public CrossMallDirectFrame(string siteId,Guid managerId, EnumConsumeSourceType consumeSourceType, int usedCount = 0)
        {
            _siteId = siteId;
            var manager = NbManagerMgr.GetById(managerId,siteId);
            if (manager != null)
            {
                _managerId = managerId;
                _account = manager.Account;
                _configMalldirectEntity = CacheFactory.MallCache.GetDirectEntity(consumeSourceType, usedCount);
                if (_configMalldirectEntity == null)
                {
                    string msg = "can't find mall direct config,consumeSourceType:" + consumeSourceType + ",usedCount:" +
                                 usedCount;
                    LogHelper.Insert(msg,LogType.Info);
                    throw new Exception(msg);
                }
            }
        }

        ~CrossMallDirectFrame()
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

        /// <summary>
        /// 检查点券数量
        /// </summary>
        /// <returns></returns>
        public MessageCode Check()
        {
            if (_configMalldirectEntity == null || _configMalldirectEntity.Point<=0)
            {
                return MessageCode.MallItemBuyFail;
            }
            PayUserEntity = PayCore.Instance.GetPayUser(_account,_siteId);
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
        public MessageCode Save(string orderId)
        {
            if (!_hasCheck)
            {
                var checkCode = Check();
                if (checkCode != MessageCode.Success)
                    return checkCode;
            }
            return PayCore.Instance.ConsumePointForGamble(_account, _managerId,
                                                          _configMalldirectEntity.ConsumeSourceType, orderId,_configMalldirectEntity.Point,_siteId);
        }
        #endregion

        #region encapsulation

        #endregion
    }
}
