using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class StatisticClient
    {
        private static IStatisticService proxy = ServiceProxy<IStatisticService>.Create();
    }
}
