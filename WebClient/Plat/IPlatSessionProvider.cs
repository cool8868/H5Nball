using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Plat
{
    public interface IPlatSessionProvider
    {
        IPlatSessionData LoginSession(string colUid,int sessionMode = -1, params string[] colArgs);
        IPlatSessionData LoadSession(string colUid, int sessionMode = -1, params string[] colArgs);
        IPlatSessionData InitSession(string colUid, params string[] colArgs);
        bool LoadSession(IPlatSessionData session, int sessionMode = -1);
        bool SaveSession(IPlatSessionData session, int sessionMode = -1);
    }
}
