using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WpfEmulator.Entity
{
    [Serializable]
    public class WpfReturnMessage
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class LoginResult
    {
        public int Code { get; set; }
        public string Cookie { get; set; }
    }
}
