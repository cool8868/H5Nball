using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.CrossServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            if (args != null && args.Contains("-debug"))
            {
                CrossService service = new CrossService();
                service.StartService(args);
                Console.WriteLine("press any key to exit");
                Console.ReadLine();
                service.StopService();
                service.Dispose();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new CrossService()  
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
