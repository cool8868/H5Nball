using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DicManagerwillparttipsEntity
	{
        [DataMember]
        public string LinkPid
        {
            get;
            set;
        }
	}


    public class DicManagerwillparttipsData
    {
        public int ItemCode
        {
            get;
            set;
        }
    }
	
	
    public partial class DicManagerwillparttipsResponse
    {

    }
}

