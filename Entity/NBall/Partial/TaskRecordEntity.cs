using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class TaskRecordEntity
	{
        private string[] _stepArray = null;
        /// <summary>
        /// 获取任务执行情况数组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string[] GetStepArray(int count)
        {
            if (_stepArray == null)
            {
                if (!string.IsNullOrEmpty(StepRecord))
                {
                    _stepArray = StepRecord.Split(',');
                }
                else
                {
                    _stepArray = new string[count];
                }
            }
            else if (_stepArray.Length < count)
            {
                var newArray = new string[count];
                for (int i = 0; i < _stepArray.Length; i++)
                {
                    newArray[i] = _stepArray[i];
                }
                _stepArray = newArray;
            }
            return _stepArray;
        }

        public bool IsPending { get; set; }
        public bool IsFromPending { get; set; }
	}
	
	
    public partial class TaskRecordResponse
    {

    }
}

