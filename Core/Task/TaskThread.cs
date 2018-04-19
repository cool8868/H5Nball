using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NBall.Core.Task
{
    public class TaskThread
    {
        ConcurrentDictionary<Guid, List<TaskRecordEntity>> _taskDic;
        private ConcurrentDictionary<Guid, ConcurrentDictionary<int, List<TaskRecordEntity>>> _taskRequireDic;
    }
}
