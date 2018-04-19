using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Games.NBall.ServiceContract.IService.IService;
using Games.NBall.Entity;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class TestService:ITestService
    {
        public String GetTestData(){
            return "数据";
        }
    }
}
