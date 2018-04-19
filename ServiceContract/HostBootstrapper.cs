using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.ServiceContract.Bootstrapper;

namespace Games.NBall.ServiceContract
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class HostBootstrapper
    {
        private static object syncLock = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Startup()
        {
            lock (syncLock)
            {
                //RandomHelper.Initialize();
                ServicesSection serviesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;

                if (serviesSection != null)
                {
                    for (int i = 0; i < serviesSection.Services.Count; i++)
                    {
                        switch (serviesSection.Services[i].Name)
                        {
                            case "Games.NBall.ServiceContract.Service.ArenaService":
                                new ArenaBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.PlayerKillService":
                                new PlayerKillBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.ManagerService":
                                new ManagerBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.OnlineService":
                                new OnlineBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.ItemService":
                                new ItemBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.MailService":
                                new MailBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.ManagerSkillService":
                                Games.NBall.Core.ManagerSkill.ManagerSkillRules.StartService();
                                break;
                            case "Games.NBall.ServiceContract.Service.TeammemberService":
                                new TeammemberBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.TrainThreadService":
                                new TrainThreadBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.LadderService":
                                new LadderBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.DailycupService":
                                new DailycupBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.SkillCardService":
                                Games.NBall.Core.SkillCard.SkillCardRules.StartService();
                                break;
                            case "Games.NBall.ServiceContract.Service.FriendService":
                                new FriendBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.RankService":
                                new RankBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.TaskService":
                                new TaskBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.ActivityService":
                                new ActivityBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.GambleService":
                                new GambleBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.StatisticService":
                                new StatisticBootstrapper().Startup();
                                break;
                            case "Games.NBall.ServiceContract.Service.CrossDataService":
                                new CrossDataBootstrapper().Startup();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
