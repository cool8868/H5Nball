
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class TaskRecordMgr
    {
        public static bool Add(TaskRecordEntity taskRecordEntity,
                              DbTransaction trans = null, string zoneId = "")
        {
            if (taskRecordEntity.IsFromPending)
            {
                return Update(taskRecordEntity, trans, zoneId);
            }
            else
            {
                int returncode = -2;
                int idx = taskRecordEntity.Idx;
                Add(taskRecordEntity.ManagerId, taskRecordEntity.TaskId, taskRecordEntity.CurTimes,
                           taskRecordEntity.StepRecord,
                           taskRecordEntity.DoneParam, taskRecordEntity.ManagerLevel, taskRecordEntity.Status, taskRecordEntity.RowTime,
                           taskRecordEntity.UpdateTime, ref idx, ref returncode, trans, zoneId);
                taskRecordEntity.Idx = idx;
                return returncode == 0;
            }
        }
	}
}

