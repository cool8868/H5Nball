using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Plat
{
    [Flags]
    public enum EnumPlatSessionMode
    {
        Cookie = 0,
        Session = 1,
        Db = 2,
    }
}
