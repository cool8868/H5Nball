using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
namespace Games.NBall.ServiceContract
{
    /// <summary>
    /// 资源清理
    /// </summary>
    public class HostFinallizer
    {
        private static object syncLock = new object();

        /// <summary>
        /// 结束
        /// </summary>
        public void Terminate()
        {
            lock (syncLock)
            {
                ServicesSection serviesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;

                if (serviesSection != null)
                {
                    for (int i = 0; i < serviesSection.Services.Count; i++)
                    {
                        switch (serviesSection.Services[i].Name)
                        {

                            default:
                                break;
                        }
                    }
                }

            }
        }
    }
}
