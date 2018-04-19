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

namespace Games.NBall.WpfEmulator
{
    /// <summary>
    /// MatchTest.xaml 的交互逻辑
    /// </summary>
    public partial class UnitMatchTest : Window
    {
        public UnitMatchTest()
        {
            InitializeComponent();
            InitZoneDic();
            CboxZones.ItemsSource = _zonesList;
            CboxZones.SelectedIndex = 0;
            
        }

        private List<Thread> _threadList;
        private List<UnitTestEntity> _unitTestEntity;
        private Dictionary<string, string> _zoneDic = new Dictionary<string, string>();
        private NameValueCollection _nc = (NameValueCollection)ConfigurationSettings.GetConfig("Zones");
        private List<string> _zonesList = new List<string>();

        private void InitZoneDic()
        {
            foreach (var key in _nc.AllKeys)
            {
                _zoneDic.Add(key, _nc[key]);
                _zonesList.Add(key);
            }
        }

        private void Start()
        {
            _threadList = new List<Thread>(_zoneDic.Count);
            _unitTestEntity = new List<UnitTestEntity>(_zoneDic.Count);
            Dictionary<string, string>.KeyCollection keys = _zoneDic.Keys;
            DataGridMatchList.Items.Clear();
            lblState.Content = "运行中";
            foreach (var zone in keys)
            {
                UnitOnLineTest matchTest = new UnitOnLineTest(zone, _zoneDic[zone], txtAccount.Text, CreateCallBack);
                Thread thread = new Thread(() => matchTest.StartMatch());
                thread.IsBackground = true;
                thread.Start();
                _threadList.Add(thread);
            }
        }

        private void CreateCallBack(string serverName, string serverState, string matchResponse, string lotteryResponse)
        {
            Dispatcher.Invoke((Action)delegate { doCreateCallBack(serverName, serverState, matchResponse, lotteryResponse); });
        }

        private void doCreateCallBack(string serverName, string serverState, string matchResponse, string lotteryResponse)
        {
            UnitTestEntity entity = new UnitTestEntity(serverName, serverState, matchResponse, lotteryResponse);
            _unitTestEntity.Add(entity);
            DataGridMatchList.Items.Add(entity);
        }

        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {         
            Start();
        }

        private void CboxZones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblUrl.Content = _nc[CboxZones.SelectedItem.ToString()] + "passport.aspx";
        }

        private void btnSingleMatch_Click(object sender, RoutedEventArgs e)
        {
            tbMatchResult.Text = "比赛中";
            tbLotteryResult.Text = "等待抽卡";
            UnitOnLineTest matchTest = new UnitOnLineTest(CboxZones.SelectedItem.ToString(), _nc[CboxZones.SelectedItem.ToString()], txtAccount.Text, CreateSingleCallBack);
            Thread thread = new Thread(() => matchTest.StartMatch());
            thread.IsBackground = true;
            thread.Start();
        }

        private void CreateSingleCallBack(string serverName, string serverState, string matchResponse, string lotteryResponse)
        {
            Dispatcher.Invoke((Action)delegate { doCreateSingleCallBack(serverName, serverState, matchResponse, lotteryResponse); });
        }

        private void doCreateSingleCallBack(string serverName, string serverState, string matchResponse, string lotteryResponse)
        {
            tbMatchResult.Text = "比赛结果："+ "\r\n" + matchResponse;
            tbLotteryResult.Text = "抽卡结果：" + "\r\n" + lotteryResponse;
            //GBoxLotteryResult.Content = lotteryResponse;
        }

    }
}
