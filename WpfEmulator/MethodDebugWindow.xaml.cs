using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games.NBall.Bll.Share;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator
{
    /// <summary>
    /// MethodDebugWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MethodDebugWindow : Window
    {
        public MethodDebugWindow()
        {
            InitializeComponent();
            TxtAccount.Text = "wxy";
        }

        private readonly List<Zone> _loginZones = new List<Zone>();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            ListBoxZones.Items.Clear();
            _loginZones.Clear();

            NameValueCollection ncZones = (NameValueCollection)ConfigurationSettings.GetConfig("Zones");

            foreach (var zone in ncZones.AllKeys)
            {
                Zone zoneInfo = new Zone(zone, ncZones[zone], TxtAccount.Text, LogInCallBack, MethodRequestCallBack);
                _loginZones.Add(zoneInfo);
                Thread thread = new Thread(zoneInfo.Login);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void LogInCallBack(string response)
        {
            Dispatcher.Invoke((Action)(() => DoLogIn(response)));
        }

        private void DoLogIn(string response)
        {
            ListBoxZones.Items.Add(response);
        }

        private void MethodRequestCallBack(string response)
        {
            Dispatcher.Invoke((Action)(() => DoMethodRequest(response)));
        }

        private void DoMethodRequest(string response)
        {
            ListBoxResponse.Items.Add(response);
        }


        private void CmbModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void CmbAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private string GetActionParams(string action)
        {
            NameValueCollection nc = (NameValueCollection) ConfigurationSettings.GetConfig("Methods");

            if (nc.AllKeys.Contains(action))
            {
                return nc[action].Trim(' ').Replace(',', '&');
            }
            return "Methods不包含方法:" + action + " 的请求参数，由配置文件内添加";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBoxResponse.Items.Clear();
        }    
    }
}
