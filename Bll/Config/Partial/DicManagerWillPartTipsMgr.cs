
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class DicManagerwillparttipsMgr
    {
        public static List<DicManagerwillparttipsData> GetWillItemcodes()
        {
            var provider = new DicManagerwillparttipsProvider();
            return provider.GetWillItemcodes();
        }
	}
}

