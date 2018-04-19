using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;

namespace Games.NBall.Core.Revelation
{
    /// <summary>
    /// 球星启示录关卡进度 处理客户端的请求
    /// </summary>
    public class RevelationCheckpointCore
    {
        private static RevelationCheckpointCore instance;
        private static object __LOCK__ = new object();
        private RevelationCheckpointCore() { }
        /// <summary>
        /// 静态构造方法 返回本类的实例
        /// </summary>
        public static RevelationCheckpointCore Instance 
　　    { 
　　        get 
　　        {
              if (instance == null)
              {
                  lock (__LOCK__)
                  {
                      if(instance == null)
                          instance = new RevelationCheckpointCore();
                  }
              }
　　            return instance; 
　　        } 
　　    }
        /// <summary>
        /// 根据经理id和关卡插入数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public bool InsertInTo(Guid managerId, int mark)
        {
            try
            {
                RevelationCheckpointEntity entity = new RevelationCheckpointEntity();
                DateTime date = Convert.ToDateTime("1970-01-01");
                entity.AwaryItem = "";
                entity.CustomsPass = mark;
                entity.GeneralAwaryTime = date;
                entity.GeneralTime = date;
                entity.IsGeneral = false;
                entity.IsGeneralAwary = false;
                entity.ManagerId = managerId;
                entity.Schedule = 0;
                entity.States = 0;
                entity.ToDayGeneralNums = 0;
                return RevelationCheckpointMgr.Insert(entity);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("新增球星启示录关卡进度", ex);
                return false;
            }
        }
    }
}
