using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Schedule
{
    public class BaseScheduleLauncher
    {
        public virtual MessageCode Excute()
        {
            throw new NotImplementedException();
        }
    }

    public class ScheduleLauncher : BaseScheduleLauncher
    {
        private ScheduleManager.ScheduleDelegate _scheduleDelegate;
        public ScheduleLauncher(ScheduleManager.ScheduleDelegate scheduleDelegate)
        {
            _scheduleDelegate = scheduleDelegate;
        }

        public override MessageCode Excute()
        {
            return _scheduleDelegate();
        }
    }

    public class ScheduleLauncherParam : BaseScheduleLauncher
    {
        private ScheduleManager.ScheduleDelegateParam _scheduleDelegate;
        private string _param;
        public ScheduleLauncherParam(ScheduleManager.ScheduleDelegateParam scheduleDelegate, string param)
        {
            _scheduleDelegate = scheduleDelegate;
            _param = param;
        }

        public override MessageCode Excute()
        {
            return _scheduleDelegate(_param);
        }
    }

    public class ScheduleLauncherNextTime : BaseScheduleLauncher
    {
        private ScheduleManager.ScheduleDelegateNextTime _scheduleDelegate;
        private int _interval;
        public ScheduleLauncherNextTime(ScheduleManager.ScheduleDelegateNextTime scheduleDelegate, int interval)
        {
            _scheduleDelegate = scheduleDelegate;
            _interval = interval + 5;
        }

        public override MessageCode Excute()
        {
            return _scheduleDelegate(DateTime.Now.AddSeconds(_interval));
        }
    }
}
