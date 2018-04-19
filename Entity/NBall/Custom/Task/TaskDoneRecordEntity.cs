using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom.Task
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class TaskDoneRecordEntity
    {
        public TaskDoneRecordEntity()
        {
            
        }

        public TaskDoneRecordEntity(int count)
        {
            Records = new List<TaskDoneParamEntity>(count);
            for (int i = 0; i < count; i++)
            {
                Records.Add(new TaskDoneParamEntity());
            }
        }

        [ProtoMember(1)]
        public List<TaskDoneParamEntity> Records { get; set; }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class TaskDoneParamEntity
    {
        public TaskDoneParamEntity()
        {
            Params=new List<string>();
        }

        public void Add(string p)
        {
            Params.Add(p);
        }

        [ProtoMember(1)]
        public List<string> Params { get; set; }
    }
}
