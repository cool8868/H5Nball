using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Chatting.Models.Sockets;
using Games.Chatting.Services;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ChattingFacade
{
    public class ChattingProxy
    {
        private static IChattingService proxy = ServiceProxy<IChattingService>.Create();
        private static string _zoneName = ConfigurationManager.AppSettings["ZoneName"].ToLower();
        private readonly string _channelName = "G";
        /// <summary>
        /// 发送系统频道消息
        /// </summary>
        /// <param name="data"></param>
        public void SendSystemChannelMessage(SocketMessageSystemChannelMsg data)
        {
            proxy.SendSystemChannelMessage(data);
        }

        /// <summary>
        /// 发送弹出消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="popType"></param>
        /// <param name="content"></param>
        public void SendPopChannelMessage(Guid managerId,int popType,string content)
        {
            SendPopChannelMessage(managerId,popType,content,_zoneName);
        }

        /// <summary>
        /// 发送Banner消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="bannerType"></param>
        /// <param name="content"></param>
        public void SendBannerChannelMessage(Guid managerId, int bannerType, string content)
        {
            SendBannerChannelMessage(managerId,bannerType,content,_zoneName);
        }

        /// <summary>
        /// 发送弹出消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="popType"></param>
        /// <param name="content"></param>
        public void SendPopChannelMessage(Guid managerId, int popType, string content,string zoneName)
        {
            SocketMessagePopChannelMsg data = new SocketMessagePopChannelMsg(zoneName, managerId.ToString(), popType, content);
            proxy.SendPopChannelMessage(data);
        }

        /// <summary>
        /// 发送Banner消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="bannerType"></param>
        /// <param name="content"></param>
        public void SendBannerChannelMessage(Guid managerId, int bannerType, string content, string zoneName)
        {
            SocketMessageBannerChannelMsg data = new SocketMessageBannerChannelMsg(zoneName, managerId.ToString(), bannerType, content);
            proxy.SendBannerChannelMessage(data);
        }

        /// <summary>
        /// 发送群组消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="managerName"></param>
        /// <param name="message"></param>
        public void SendGroupChannelMessage(Guid managerId, string managerName, string message,int vipLevel)
        {
            SocketMessageGroupChannelMsg data = new SocketMessageGroupChannelMsg(
                _zoneName, managerId.ToString(), managerName,
                _zoneName, _channelName,
                message, vipLevel.ToString());
            proxy.SendGroupChannelMessage(data);
        }

        #region 发送公会消息
        /// <summary>
        /// 发送公会消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="managerName"></param>
        /// <param name="guildId"></param>
        /// <param name="message"></param>
        public void SendGuildChannelMessage(Guid managerId, string managerName, Guid guildId, string message)
        {
            SocketMessageGroupChannelMsg data = new SocketMessageGroupChannelMsg(
                _zoneName, managerId.ToString(), managerName,
                _zoneName, string.Concat("GH", guildId.ToString("N")),
                message, "");
            proxy.SendGroupChannelMessage(data);
        }
        #endregion

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="managerName"></param>
        /// <param name="targetId"></param>
        /// <param name="targetName"></param>
        /// <param name="message"></param>
        public void SendPrivateChannelMessage(Guid managerId, string managerName,string targetId,string targetName, string message)
        {
            SocketMessagePrivateChannelMsg data = new SocketMessagePrivateChannelMsg(
                _zoneName,managerId.ToString(),managerName,_zoneName,targetId,targetName,message,"");
            proxy.SendPrivateChannelMessage(data);
        }
    }
}
