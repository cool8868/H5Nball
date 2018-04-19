using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.BLF;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.ServiceEngine;

namespace Games.NBall.MatchFacade
{
    public class MatchClient 
    {
        private static IMatchService proxy = ServiceProxy<IMatchService>.Create();
        
       /// <summary>
       /// 
       /// </summary>
       /// <param name="matchEntity"></param>
       /// <param name="transferMatchEntity"></param>
       /// <returns></returns>
        public byte[] CreateMatch(MatchInput matchInput)
        {
            //var watch = new Stopwatch();
            //watch.Start();
            var resultEntityByte = proxy.CreateMatchToBin(matchInput);
            //watch.Stop();
            //SysteminfologMgr.Insert("Server-CreateMatchNewInterface", "Time:[" + watch.ElapsedMilliseconds.ToString("N0") + "ms]");
            //CodeTimer outputmatchTimer = new CodeTimer("ResolverEngine-Convert");
            //outputmatchTimer.Record();

            return resultEntityByte;
        }
    }
}
