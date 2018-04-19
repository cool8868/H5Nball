using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class StatisticKpiEntity
	{
        private string _s = "";

        public string RecordDateStr
        {
            get
            {
                if (string.IsNullOrEmpty(_s))
                    return RecordDate.ToString("yyyy-MM-dd");
                return _s;
            }
            set
            {
                _s = value;
            }
        }

        public long AvgOnline { get { return Dau == 0 ? 0 : TotalOnline / Dau; } }

        public int ARPU { get; set; }

        public int ARRPU { get; set; }

        public int PayRate { get; set; }

        public int PayUserLost { get; set; }

        public int PayLost { get; set; }

        public int LTV { get; set; }

        public string RetentionPercent2 { get; set; }

        public string RetentionPercent3 { get; set; }

        public string RetentionPercent4 { get; set; }

        public string RetentionPercent5 { get; set; }

        public string RetentionPercent6 { get; set; }

        public string RetentionPercent7 { get; set; }
        public string RetentionPercent15 { get; set; }

        public string RetentionPercent30 { get; set; }
	}
	
	
    public partial class StatisticKpiResponse
    {

    }
}
