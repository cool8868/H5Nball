using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NBall.NUnitTest
{
    public struct NUnitConstants
    {
        public static NbManagerEntity Manager { get; set; }
        public static Guid TestManagerGuid { get { return Manager==null?Guid.Empty:Manager.Idx; } }
        public readonly static string TestAccount = "#NunitTest";
    }
}
