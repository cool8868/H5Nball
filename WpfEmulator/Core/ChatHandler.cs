using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Games.Chatting.Client;
using Games.Chatting.Models;
using Games.Chatting.Models.Sockets;
using Games.NBall.Common;

namespace Games.NBall.WpfEmulator.Core
{
    public class ChatHandler
    {
        public ChatHandler(ShowMessageDelegate showMessageDelegate)
        {
            _showMessageDelegate = showMessageDelegate;
        }

        public delegate void ShowMessageDelegate(string message);

        public void Start()
        {
            try
            {
                var thread = new Thread(() => ThreadClient());
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                _showMessageDelegate("线程启动失败"+ex.Message);
            }
        }

        private ShowMessageDelegate _showMessageDelegate;
        void ThreadClient()
        {
            try
            {
                string zoneName = ConfigurationManager.AppSettings["ZoneName"];
                string channel = ConfigurationManager.AppSettings["ChatChannelId"];
                string RoleID = ApiTestCore.CurManager.Idx.ToString();
                ChattingClient client = new ChattingClient();
                client.Connect(zoneName, RoleID);
                client.OnBackgroundThreadException += new BackgroundThreadExceptionEventHandler(client_OnBackgroundThreadException);
                client.OnReceiveGroupChannelMessage += new ChattingClient.ReceiveGroupChannelMessageEventHandler(client_OnReceiveGroupChannelMessage);
                client.OnReceivePrivateChannelMessage += new ChattingClient.ReceivePrivateChannelMessageEventHandler(client_OnReceivePrivateChannelMessage);
                client.OnReceiveSystemChannelMessage += new ChattingClient.ReceiveSystemChannelMessageEventHandler(client_OnReceiveSystemChannelMessage);
                
                client.OnReceivePopChannelMessage +=
                    new ChattingClient.ReceivePopChannelMessageEventHandler(client_OnReceivePopChannelMessage);
                client.OnReceiveBannerChannelMessage +=
                    new ChattingClient.ReceiveBannerChannelMessageEventHandler(client_OnReceiveBannerChannelMessage);
                try
                {
                    string serverIPPort;
                    string ticket = client.GetLoginTicket(zoneName, RoleID, out serverIPPort);
                    client.Login(ticket, zoneName, RoleID);
                    LogHelper.Insert("Login Success", LogType.Info);
                }
                catch (Exception ex)
                {
                    _showMessageDelegate(ex.Message);
                    throw ex;
                }
                _showMessageDelegate(RoleID + " Login Success!");
                Thread.Sleep(0);
                try
                {
                    string ticket = client.GetJoinTicket(zoneName, channel);
                    client.RoleJoinChannel(zoneName, RoleID, zoneName, channel);
                    LogHelper.Insert("Chatting Server:" + zoneName + " Channel：" + channel, LogType.Info);
                }
                catch (Exception ex)
                {
                    _showMessageDelegate(ex.Message);
                    LogHelper.Insert(ex);
                }
                _showMessageDelegate(RoleID + "Join Channel Success!");
                _showMessageDelegate("Please Chatting:");
                Thread.Sleep(0);
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex1)
            {
                _showMessageDelegate(ex1.Message);
                LogHelper.Insert(ex1);
            }
            
        }

        void client_OnReceiveSystemChannelMessage(object sender, ChattingClient.ReceiveSystemChannelMessageEventArgs e)
        {
            DateTime nowTime = DateTime.Now;
            // DateTime sendTime = DateTime.Parse(e.Message.MessageText);
            // Console.WriteLine(((TimeSpan)(nowTime.Subtract(sendTime))).Milliseconds.ToString());
            _showMessageDelegate("System: " + e.Message.MessageText);
            //Console.WriteLine("ReceiveSystemChannelMessage: " + e.Message.MessageText);
        }

        void client_OnReceivePopChannelMessage(object sender, ChattingClient.ReceivePopChannelMessageEventArgs e)
        {
            DateTime nowTime = DateTime.Now;
            // DateTime sendTime = DateTime.Parse(e.Message.MessageText);
            // Console.WriteLine(((TimeSpan)(nowTime.Subtract(sendTime))).Milliseconds.ToString());
            _showMessageDelegate("[Pop]popType(" + e.Message.PopType + "), content(" + e.Message.MessageText+")");
            //Console.WriteLine("ReceiveSystemChannelMessage: " + e.Message.MessageText);
        }

        void client_OnReceiveBannerChannelMessage(object sender, ChattingClient.ReceiveBannerChannelMessageEventArgs e)
        {
            DateTime nowTime = DateTime.Now;
            // DateTime sendTime = DateTime.Parse(e.Message.MessageText);
            // Console.WriteLine(((TimeSpan)(nowTime.Subtract(sendTime))).Milliseconds.ToString());
            _showMessageDelegate("[Banner]bannerType(" + e.Message.BannerType + "), content(" + e.Message.MessageText + ")");
            //Console.WriteLine("ReceiveSystemChannelMessage: " + e.Message.MessageText);
        }

        void client_OnReceivePrivateChannelMessage(object sender, ChattingClient.ReceivePrivateChannelMessageEventArgs e)
        {
            DateTime nowTime = DateTime.Now;
            //DateTime sendTime = DateTime.Parse(e.Message.MessageText);
            //Console.WriteLine(((TimeSpan)(nowTime.Subtract(sendTime))).Milliseconds.ToString());
            //Console.WriteLine("私密-" + e.Message.SenderRoleName + ":" + e.Message.MessageText);
            _showMessageDelegate("[私聊]" + e.Message.SenderRoleName + ":" + e.Message.MessageText);
        }

        void client_OnReceiveGroupChannelMessage(object sender, ChattingClient.ReceiveGroupChannelMessageEventArgs e)
        {
            DateTime nowTime = DateTime.Now;
            // DateTime sendTime = DateTime.Parse(e.Message.MessageText);
            //Console.WriteLine(((TimeSpan)(nowTime.Subtract(sendTime))).Milliseconds.ToString());

            //Console.WriteLine(e.Message.SenderRoleName + ":" + e.Message.MessageText);
            _showMessageDelegate("[世界]" + e.Message.SenderRoleName + ":" + e.Message.MessageText);
        }

        void client_OnBackgroundThreadException(object sender, BackgroundThreadExceptionEventArgs e)
        {
            //Console.WriteLine(e.ThreadException.Message);
            _showMessageDelegate(e.ThreadException.Message);
        }
    }
}
