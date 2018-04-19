using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using System.Web.SessionState;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.NB_Web.BaseCommon;

namespace Games.NBall.NB_Web.Helper
{
    internal class CommandHandler : IHttpHandler, IRequiresSessionState
    {

        #region IHttpHandler Members

        /// <summary>
        /// 通过实现 <see cref="T:System.Web.IHttpHandler"/> 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
        /// </summary>
        /// <param name="context"><see cref="T:System.Web.HttpContext"/> 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [wangweiqiang]     2010-2-3 16:04     Created
        /// </history>
        public void ProcessRequest(HttpContext context)
        {
            string commandName = "";
            try
            {
                Uri url = context.Request.Url;
                string fileName = url.Segments.Last();
                string extension = fileName.Split('.').Last();
                commandName = fileName.Remove(fileName.Length - extension.Length - 1);
                string action = context.Request["action"];
                
                if (string.IsNullOrEmpty(commandName) || string.IsNullOrEmpty(action))
                {
                    OutputHelper.OutputBadRequest();
                }
                else
                {
                    commandName = commandName.ToLower();
                    action = action.ToLower();
                    switch (commandName)
                    {
                        case "ar":
                            new Command.ArenaCommand().Dispatch(action);
                            break;
                        case "car":
                            new Command.CrossArenaCommand().Dispatch(action);
                            break;
                        case "ac":
                            new Command.ActivityCommand().Dispatch(action);
                            break;
                        case "pl":
                            new Command.PlayerKillCommand().Dispatch(action);
                            break;
                        case "manager":
                            new Command.ManagerCommand().Dispatch(action);
                            break;
                        case "item":
                            new Command.ItemCommand().Dispatch(action);
                            break;
                        case "mi":
                            new Command.MailCommand().Dispatch(action);
                            break;
                        case "fr":
                            new Command.FriendCommand().Dispatch(action);
                            break;
                        case "mskill":
                            new Command.ManagerSkillCommand().Dispatch(action);
                            break;
                        case "teammember":
                            new Command.TeammemberCommand().Dispatch(action);
                            break;
                        case "scouting":
                            new Command.ScoutingCommand().Dispatch(action);
                            break;
                        case "league":
                            new Command.LeagueCommand().Dispatch(action);
                            break;
                        case "mall":
                            new Command.MallCommand().Dispatch(action);
                            break;
                        case "train":
                            new Command.TrainCommand().Dispatch(action);
                            break;
                        case "ladder":
                            new Command.LadderCommand().Dispatch(action);
                            break;
                        case "dailycup":
                            new Command.DailycupCommand().Dispatch(action);
                            break;
                        case "matchdata":
                            new Command.MatchDataCommand().Dispatch(action);
                            break;
                        case "skillcard":
                            new Command.SkillCardCommand().Dispatch(action);
                            break;
                        case "rk":
                            new Command.RankCommand().Dispatch(action);
                            break;
                        case "tk":
                            new Command.TaskCommand().Dispatch(action);
                            break;
                        case "gamble":
                            new Command.GambleCommand().Dispatch(action);
                            break;
                        case "cc":
                            new Command.CrossCrowdCommand().Dispatch(action);
                            break;
                        case "cl":
                            new Command.CrossLadderCommand().Dispatch(action);
                            break;
                        case "tf":
                            new Command.TransferCommand().Dispatch(action);
                            break;

                        default:
                            OutputHelper.OutputBadRequest();
                            break;
                    }
                }
            }
            catch (TargetInvocationException tIEx)
            {
                LogEx(commandName, tIEx, context);
            }
            catch (EndpointNotFoundException eNFEx)
            {
                LogEx(commandName, eNFEx, context);
            }
            catch (SocketException sktEx)
            {
                LogEx(commandName, sktEx, context);
            }
            catch (NullReferenceException nullEx)
            {
                LogEx(commandName, nullEx, context);
            }
            catch (ArgumentOutOfRangeException aOutREx)
            {
                LogEx(commandName, aOutREx, context);
            }
            catch (ArgumentNullException anullEx)
            {
                LogEx(commandName, anullEx, context);
            }
            catch (NotImplementedException nIEx)
            {
                LogEx(commandName, nIEx, context);
            }
            catch (TimeoutException tmEx)
            {
                LogEx(commandName, tmEx, context);
            }
            catch (OutOfMemoryException oomEx)
            {
                LogEx(commandName, oomEx, context);
            }
            catch (Exception ex)
            {
                LogEx(commandName, ex, context);
            }
            
        }

        static void LogEx(string commandName, Exception ex, HttpContext context)
        {
            try
            {
                SystemlogMgr.Error("Global-ProcessRequest", string.Format("Command:[{1}],Message:[{0}]", ex.Message, commandName), ex.StackTrace);
            }
            catch (Exception ex1)
            {
                LogHelper.Insert(ex1);
            }
            context.Response.Clear();
            OutputHelper.OutputException();
        }

        /// <summary>
        /// 获取一个值，该值指示其他请求是否可以使用 <see cref="T:System.Web.IHttpHandler"/> 实例。
        /// </summary>
        /// <value></value>
        /// <returns>
        /// 如果 <see cref="T:System.Web.IHttpHandler"/> 实例可再次使用，则为 true；否则为 false。</returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [wangweiqiang]     2010-2-3 16:03     Created
        /// </history>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}