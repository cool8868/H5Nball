
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TeammemberTrainMgr
    {
        public static void SaveTrainData(TeammemberTrainEntity trainEntity, ref int returnCode, DbTransaction trans = null)
        {
            UpdateData(trainEntity.Idx, trainEntity.Level, trainEntity.EXP, trainEntity.TrainStamina,
                                          trainEntity.TrainState,
                                          trainEntity.StartTime, trainEntity.SettleTime, trainEntity.Status,
                                          ShareUtil.GetTableMod(trainEntity.ManagerId), ref returnCode,
                                          trans);

        }
	}
}

