using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Entity;
using Games.NBall.Bll.Share;

namespace Games.NBall.Core.Ladder
{
    public  class LadderDayPrize
    {

          #region .ctor

        public static LadderDayPrize Instance
        {
            get { return SingletonFactory<LadderDayPrize>.SInstance; }
        }

        public LadderDayPrize(int p)
        {
            Initialize();
        }
        #endregion

        private void Initialize()
        {

        }

        /// <summary>
        /// 获取用户天梯每日奖励信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderDayprizeEntity GetManagerInfo(Guid managerId)
        {
            try
            {
                var info = LadderDayprizeMgr.GetById(managerId);
                if (info == null)
                    info = InsertLadderDayPrize(managerId);
                if (info.UpdateTime.Date != DateTime.Now)
                {
                    info.WinNumber = 0;
                    info.PrizeRecord = "0,0,0,0";
                    info.UpdateTime = DateTime.Now;
                    LadderDayprizeMgr.Update(info);
                }
                return info;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取天梯每日获胜奖励信息", ex);
            }
            return null;
        }

        /// <summary>
        /// 添加一个胜场
        /// </summary>
        /// <param name="managerId"></param>
        public void AddWinNumber(Guid managerId)
        {
            try
            {
                var entity = GetManagerInfo(managerId);
                if (entity == null)
                    return;
                entity.WinNumber += 1;
                entity.UpdateTime = DateTime.Now;
                LadderDayprizeMgr.Update(entity);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("天梯赛每日胜场加一场", ex);
            }
        }

        public void WinPrizeGet(Guid managerId)
        {

        }

        public LadderDayprizeEntity InsertLadderDayPrize(Guid managerId)
        {
            LadderDayprizeEntity entity = new LadderDayprizeEntity();
            entity.ManagerId = managerId;
            entity.PrizeRecord = "0,0,0,0";
            entity.RowTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            entity.WinNumber = 0;
            LadderDayprizeMgr.Insert(entity);
            return entity;
        }
    }
}
