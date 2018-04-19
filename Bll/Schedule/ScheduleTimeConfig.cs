using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;

namespace Games.NBall.Bll.Schedule
{
    public class BaseScheduleTimeConfig
    {
        public DateTime InvokeTime { get; set; }

        public virtual DateTime GetNext(DateTime now)
        {
            return now;
        }

        public virtual int GetIntervalTime()
        {
            return 0;
        }
    }

    public class ScheduleTimeConfigTimeTable : BaseScheduleTimeConfig
    {
        public ScheduleTimeConfigTimeTable(string setting,bool isChampion=false)
        {
            string[] parts = setting.Split(' ');
            if(parts.Length!=5)
                throw new Exception("Schedule time init fail,setting length:"+parts.Length);
            if (isChampion)
                _isMonthFirst = true;
            Minutes = BuildIterator(parts[0]);
            Hours = BuildIterator(parts[1]);
            Days = BuildIterator(parts[2]);
            Months = BuildList(parts[3]);
            Weeks = BuildWeek(parts[4]);
            InvokeTime = DateTime.Now.AddMinutes(-1);
        }

        TimeIterator Minutes { get; set; }

        TimeIterator Hours { get; set; }

        TimeIterator Days { get; set; }

        List<int> Months { get; set; }

        List<DayOfWeek> Weeks { get; set; }

        private bool _isFirst = true;
        public override DateTime GetNext(DateTime now)
        {
            int minute = InvokeTime.Minute;
            int hour = InvokeTime.Hour;
            int day = InvokeTime.Day;
            int year = InvokeTime.Year;
            int month = InvokeTime.Month;
            while (CheckNext(now))
            {
                CalNext(ref minute, ref hour, ref day, ref month, ref year);
                if (!CheckDate(year, month, day,hour,minute))
                {
                    throw new Exception(string.Format("Schedule time config,cal next date is invalid,year:{0},month:{1},day:{2},hour:{3},minute:{4}", year, month, day,hour,minute));
                }
                InvokeTime = new DateTime(year,month,day,hour,minute,0);
            }
            return InvokeTime;
        }

        bool CheckNext(DateTime now)
        {
            if (InvokeTime <= now)
                return true;
            if (Months != null && !Months.Contains(InvokeTime.Month))
                return true;
            if (Weeks != null && !Weeks.Contains(InvokeTime.DayOfWeek))
                return true;
            return false;
        }

        void CalNext(ref int minute,ref int hour,ref int day,ref int month,ref int year)
        {
            #region ***分***
            if (Minutes!=null && Minutes.MoveNext())//按分钟循环
            {
                minute = Minutes.Current;
                return;
            }

            //重置
            if (Minutes != null)
            {
                Minutes.Reset();
                minute = Minutes.Current;
            }
            else
            {
                minute = 0;
            }
            #endregion

            #region ***时***
            if (Hours == null && hour < 23)//每小时循环
            {
                hour++;
                return;
            }
            else if (Hours !=null && Hours.MoveNext())
            {
                hour = Hours.Current;
                return;
            }
            //重置
            if (Hours != null)
            {
                Hours.Reset();
                hour = Hours.Current;
            }
            else
            {
                hour = 0;
            }
            #endregion

            #region ***日***
            if (Days == null)
            {
                if (_isFirst)
                {
                    _isFirst = false;
                    return;
                }
                var newD = InvokeTime.AddDays(1);
                day = newD.Day;
                month = newD.Month;
                year = newD.Year;
                return;
            }
            else if (Days.MoveNext())
            {
                day = Days.Current;
                return;
            }
            else
            {
                Days.Reset();
                day = Days.Current;
             
            }

            if (Months == null)
            {
                if (_isMonthFirst)
                {
                    _isMonthFirst = false;
                    return;
                }
                var newD = InvokeTime.AddMonths(1);
                month = newD.Month;
                year = newD.Year;
                return;
            }
            #endregion
        }

        private bool _isMonthFirst = false;

        TimeIterator BuildIterator(string part)
        {
            if ("*".Equals(part))
                return null;
            else if(part.Contains("-"))
            {
                string[] fromto = part.Split('-');
                int from = ConvertHelper.ConvertToInt(fromto[0]);
                int to = ConvertHelper.ConvertToInt(fromto[1]);
                return new TimeIterator(from,to);
            }
            else
            {
                string[] periods = part.Split(',');
                return new TimeIterator(periods);
            }
        }

        List<int> BuildList(string part)
        {
            if ("*".Equals(part))
                return null;
            else if (part.Contains("-"))
            {
                string[] fromto = part.Split('-');
                int from = ConvertHelper.ConvertToInt(fromto[0]);
                int to = ConvertHelper.ConvertToInt(fromto[1]);
                List<int> list = new List<int>(to-from+1);
                for (int i = from; i <= to; i++)
                {
                    list.Add(i);
                }
                return list;
            }
            else
            {
                string[] ss = part.Split(',');
                List<int> list = new List<int>(ss.Length);
                foreach (var s in ss)
                {
                    list.Add(ConvertHelper.ConvertToInt(s));
                }
                return list;
            }
        }

        List<DayOfWeek> BuildWeek(string part)
        {
            if ("*".Equals(part))
                return null;
            else if (part.Contains("-"))
            {
                string[] fromto = part.Split('-');
                int from = ConvertHelper.ConvertToInt(fromto[0]);
                int to = ConvertHelper.ConvertToInt(fromto[1]);
                List<DayOfWeek> list = new List<DayOfWeek>(to - from + 1);
                for (int i = from; i <= to; i++)
                {
                    list.Add(GetDayOfWeek(i));
                }
                return list;
            }
            else
            {
                string[] ss = part.Split(',');
                List<DayOfWeek> list = new List<DayOfWeek>(ss.Length);
                foreach (var s in ss)
                {
                    list.Add(GetDayOfWeek(ConvertHelper.ConvertToInt(s)));
                }
                return list;
            }

        }
        
        private DayOfWeek GetDayOfWeek(int week)
        {
            switch (week)
            {
                case 1: return  System.DayOfWeek.Monday;
                case 2: return  System.DayOfWeek.Tuesday;
                case 3: return  System.DayOfWeek.Wednesday;
                case 4: return  System.DayOfWeek.Thursday;
                case 5: return  System.DayOfWeek.Friday;
                case 6: return  System.DayOfWeek.Saturday;
                case 7: return  System.DayOfWeek.Sunday;
                default: throw new ArgumentException();
            }
        }

        /// <summary>
        /// 验证日期合法性
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private bool CheckDate(int year,int month,int day,int hour,int minute)
        {
            try
            {
                if (hour < 0 || hour > 24)
                    return false;
                if (minute < 0 || minute > 60)
                    return false;

                int[] days = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (IsLeapYear(year))
                {
                    //如果是闰年
                    days[1] = 29;
                }
                return year > 0 && month <= 12 && month > 0 && day <= days[month - 1] && day > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是闰年
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>是否是闰年</returns>
        private bool IsLeapYear(int year)
        {
            return (year % 4 == 0 || year % 400 == 0) && (year % 100 != 0);
        }
    }

    public class ScheduleTimeConfigInterval:BaseScheduleTimeConfig
    {
        int Interval { get; set; }

        public ScheduleTimeConfigInterval(string setting)
        {
            Interval = ConvertHelper.ConvertToInt(setting);
            if (Interval <= 0)
            {
                throw new Exception("ScheduleTimeConfigInterval ctor cause exception,interval:"+Interval);
            }
            InvokeTime = DateTime.Now;
        }

        public override DateTime GetNext(DateTime now)
        {
            InvokeTime = InvokeTime.AddSeconds(Interval);
            return InvokeTime;
        }

        public override int GetIntervalTime()
        {
            return Interval;
        }
    }

    public class TimeIterator
    {
        private int[] _times;
        int _position = -1;
        private int _length = 0;
        public TimeIterator(string[] tArray)
        {
            _length = tArray.Length;
            _times = new int[_length];

            for (int i = 0; i < _length; i++)
            {
                _times[i] = ConvertHelper.ConvertToInt(tArray[i]);
            }
        }

        public TimeIterator(int from,int to)
        {
            _length = to-from+1;
            _times = new int[_length];

            for (int i = from,j=0; i <= to; i++,j++)
            {
                _times[j] = i;
            }
        }

        public bool MoveNext()
        {
            if (_length == 1)
                return false;
            _position++;
            return (_position < _length);
        }

        public void Reset()
        {
            _position = 0;
        }

        public int Current
        {
            get
            {
                try
                {
                    if (_length == 1)
                        return _times[0];
                    else
                    {
                        return _times[_position];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }

    public class WeekIterator
    {
        private DayOfWeek[] _times;
        int _position = -1;
        private int _length = 0;
        public WeekIterator(string[] tArray)
        {
            _length = tArray.Length;
            _times = new DayOfWeek[_length];

            for (int i = 0; i < _length; i++)
            {
                _times[i] =GetDayOfWeek(ConvertHelper.ConvertToInt(tArray[i]));
            }
        }

        public WeekIterator(int from, int to)
        {
            _length = to - from + 1;
            _times = new DayOfWeek[_length];

            for (int i = from, j = 0; i <= to; i++, j++)
            {
                _times[j] = GetDayOfWeek(i);
            }
        }

        public bool MoveNext()
        {
            if (_length == 1)
                return false;
            _position++;
            return (_position < _length);
        }

        public void Reset()
        {
            _position = 0;
        }

        public DayOfWeek Current
        {
            get
            {
                try
                {
                    if (_length == 1)
                        return _times[0];
                    else
                    {
                        return _times[_position];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        private DayOfWeek GetDayOfWeek(int week)
        {
            switch (week)
            {
                case 1: return  System.DayOfWeek.Monday;
                case 2: return  System.DayOfWeek.Tuesday;
                case 3: return  System.DayOfWeek.Wednesday;
                case 4: return  System.DayOfWeek.Thursday;
                case 5: return  System.DayOfWeek.Friday;
                case 6: return  System.DayOfWeek.Saturday;
                case 7: return  System.DayOfWeek.Sunday;
                default: throw new ArgumentException();
            }
        }
    }
}
