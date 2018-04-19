using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.ServiceContract.Service;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract
{
    /// <summary>
    /// 服务host
    /// </summary>
    public class HostNBallService : ServiceSelfHost
    {
        /// <summary>
        /// 服务列表
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Type> GetHostServiceType()
        {

            ServicesSection serviesSection = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;

            if (serviesSection != null)
            {
                Type[] types = new Type[serviesSection.Services.Count];

                for (int i = 0; i < serviesSection.Services.Count; i++)
                {
                    switch (serviesSection.Services[i].Name)
                    {
                        case "Games.NBall.ServiceContract.Service.ArenaService":
                            types[i] = typeof(ArenaService);
                            break;
                        case "Games.NBall.ServiceContract.Service.PlayerKillService":
                            types[i] = typeof(PlayerKillService);
                            break;
                        case "Games.NBall.ServiceContract.Service.ManagerService":
                            types[i] = typeof(ManagerService);
                            break;
                        case "Games.NBall.ServiceContract.Service.OnlineService":
                            types[i] = typeof(OnlineService);
                            break;
                        case "Games.NBall.ServiceContract.Service.AdminService":
                            types[i] = typeof(AdminService);
                            break;
                        case "Games.NBall.ServiceContract.Service.MailService":
                            types[i] = typeof(MailService);
                            break;
                        case "Games.NBall.ServiceContract.Service.ItemService":
                            types[i] = typeof(ItemService);
                            break;
                        case "Games.NBall.ServiceContract.Service.ManagerSkillService":
                            types[i] = typeof(ManagerSkillService);
                            break;
                        case "Games.NBall.ServiceContract.Service.TeammemberService":
                            types[i] = typeof(TeammemberService);
                            break;
                        case "Games.NBall.ServiceContract.Service.ScoutingService":
                            types[i] = typeof(ScoutingService);
                            break;
                        case "Games.NBall.ServiceContract.Service.LeagueService":
                            types[i] = typeof(LeagueService);
                            break;
                        case "Games.NBall.ServiceContract.Service.MallService":
                            types[i] = typeof(MallService);
                            break;
                        case "Games.NBall.ServiceContract.Service.TrainThreadService":
                            types[i] = typeof(TrainThreadService);
                            break;
                        case "Games.NBall.ServiceContract.Service.LadderService":
                            types[i] = typeof(LadderService);
                            break;
                        case "Games.NBall.ServiceContract.Service.DailycupService":
                            types[i] = typeof(DailycupService);
                            break;
                        case "Games.NBall.ServiceContract.Service.MatchDataService":
                            types[i] = typeof(MatchDataService);
                            break;
                        case "Games.NBall.ServiceContract.Service.SkillCardService":
                            types[i] = typeof(SkillCardService);
                            break;
                        case "Games.NBall.ServiceContract.Service.FriendService":
                            types[i] = typeof(FriendService);
                            break;
                        case "Games.NBall.ServiceContract.Service.RankService":
                            types[i] = typeof(RankService);
                            break;
                        case "Games.NBall.ServiceContract.Service.TaskService":
                            types[i] = typeof(TaskService);
                            break;
                        case "Games.NBall.ServiceContract.Service.ActivityService":
                            types[i] = typeof(ActivityService);
                            break;
                        case "Games.NBall.ServiceContract.Service.GambleService":
                            types[i] = typeof(GambleService);
                            break;
                        case "Games.NBall.ServiceContract.Service.CrossDataService":
                            types[i] = typeof(CrossDataService);
                            break;
                        default:
                            types[i] = typeof(object);
                            break;
                    }

                }

                return types;
            }
            else
            {
                return base.GetHostServiceType();
            }


        }
    }
}
