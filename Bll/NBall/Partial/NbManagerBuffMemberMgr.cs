
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class NbManagerbuffmemberMgr
    {
        #region  GetByMid

        public static List<NbManagerbuffmemberEntity> GetByMid(System.Guid managerId, System.Int32 managerHash, string zoneId = "")
        {
            var provider = new NbManagerbuffmemberProvider(zoneId);
            return provider.GetByMid(managerId, managerHash);
        }

        #endregion

        #region  SyncSend

        public static bool SyncSend(System.Guid managerId, System.Byte[] syncRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null, string zoneId = "")
        {
            NbManagerbuffmemberProvider provider = new NbManagerbuffmemberProvider(zoneId);

            return provider.SyncSend(managerId, syncRowVersion, ref  errorCode, trans);

        }

        #endregion

        #region  SyncStart

        public static bool SyncStart(System.Guid managerId, System.Byte[] syncRowVersion, ref  System.Byte[] newRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null, string zoneId = "")
        {
            NbManagerbuffmemberProvider provider = new NbManagerbuffmemberProvider(zoneId);

            return provider.SyncStart(managerId, syncRowVersion, ref  newRowVersion, ref  errorCode, trans);

        }

        #endregion

        #region  SyncIdle

        public static bool SyncIdle(System.Guid managerId, System.Int32 managerHash, System.Guid tid, System.Guid tid2, System.Guid tid3, System.Guid tid4, System.Guid tid5, DbTransaction trans = null, string zoneId = "")
        {
            NbManagerbuffmemberProvider provider = new NbManagerbuffmemberProvider(zoneId);

            return provider.SyncIdle(managerId, managerHash, tid, tid2, tid3, tid4, tid5, trans);

        }

        #endregion

        #region  SyncInto

        public static bool SyncInto(System.Guid managerId, System.Int32 managerHash, System.Guid tid, System.Int32 pid, System.Int32 pPos, System.Int32 kpi, System.Int32 level, System.Int32 strength, System.Int32 showOrder, System.Boolean isMain, System.Double speed, System.Double shoot, System.Double freeKick, System.Double balance, System.Double physique, System.Double bounce, System.Double aggression, System.Double disturb, System.Double interception, System.Double dribble, System.Double pass, System.Double mentality, System.Double response, System.Double positioning, System.Double handControl, System.Double acceleration, DbTransaction trans = null, string zoneId = "")
        {
            NbManagerbuffmemberProvider provider = new NbManagerbuffmemberProvider(zoneId);

            return provider.SyncInto(managerId, managerHash, tid, pid, pPos, kpi, level, strength, showOrder, isMain, speed, shoot, freeKick, balance, physique, bounce, aggression, disturb, interception, dribble, pass, mentality, response, positioning, handControl, acceleration, trans);

        }

        #endregion

        #region  SyncEnd

        public static bool SyncEnd(System.Guid managerId, System.String playerSkills, System.String managerSkills, System.Byte[] syncRowVersion, ref  System.Byte[] newRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null, string zoneId = "")
        {
            NbManagerbuffmemberProvider provider = new NbManagerbuffmemberProvider(zoneId);

            return provider.SyncEnd(managerId, playerSkills, managerSkills, syncRowVersion, ref  newRowVersion, ref  errorCode, trans);

        }

        #endregion

        #region  SyncBatch

        public static bool SyncBatch(System.Guid managerId, System.Int32 kpi, System.String playerSkills, System.String managerSkills, System.Byte[] syncRowVersion, ref  System.Byte[] newRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null, string zoneId = "")
        {
            NbManagerbuffmemberProvider provider = new NbManagerbuffmemberProvider(zoneId);

            return provider.SyncBatch(managerId, kpi, playerSkills, managerSkills, syncRowVersion, ref  newRowVersion, ref  errorCode, trans);

        }

        #endregion
        
	}
}

