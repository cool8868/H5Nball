using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;

namespace Games.NBall.ServiceContract.IService.IService
{
     [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ITestService
    {
        [OperationContract]
        String GetTestData();

     
    }
}
