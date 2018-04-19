using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranOut;
using Games.NBall.Bll;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.WpfEmulator;
using Games.NBall.WpfEmulator.Command;
using Games.NBall.WpfEmulator.Component.Forms;
using Games.NBall.WpfEmulator.Component.UserControls;
using Games.NBall.WpfEmulator;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;
using Games.NBall.WpfEmulator.Tools;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;

namespace WpfEmulator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ApiTestWindow : Window
    {
        private bool _isLoad = false;
        private DateTime _serverTime;
        private DispatcherTimer _countdownTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        private DispatcherTimer _heartTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(60) };

        public string _cookie ="";
        public ApiTestWindow()
        {
            InitializeComponent();
            //RandomHelper.Initialize();
            ApiTestCore.MainWindow = this;
            RequestHelper.Initialize();
            CanvasMain.Visibility = Visibility.Hidden;
            GridLogin.Visibility = Visibility.Hidden;
            GridRegister.Visibility = Visibility.Hidden;
        }

        #region Fields

        private bool _isLogin = false;
        #endregion

        #region Event
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (_isLoad == false)
            {
                _countdownTimer.Tick += UpdateTime;
                _heartTimer.Tick += Heart;
                try
                {
                    Thread thread = new Thread(InitCache) {IsBackground = true};
                    thread.Start();
                    if (!_isLogin)
                    {
                        ShowPanels(ShowPanelType.Login);
                    }
                    else
                    {
                        ShowMain(true);
                    }
                    CacheHelper.Instance.GetHashCode();
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex);
                    MessageBox.Show(ex.Message);
                }
                
                _isLoad = true;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ApiTestCore.CurManager = null;
            ApiTestCore.CurManagerExtra = null;
            ApiTestCore.CurPoint = 0;
            ShowPanels(ShowPanelType.Login);
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItemCreateBot_OnClick(object sender, RoutedEventArgs e)
        {
            CreateBotWindow window = new CreateBotWindow();
            window.Show();
        }

        private void MenuItemRegisterBot_OnClick(object sender, RoutedEventArgs e)
        {
            RegisterBotWindow window = new RegisterBotWindow();
            window.Show();
        }

        private void MenuItemRefreshManager_Click(object sender, RoutedEventArgs e)
        {
            if(!_isLogin)
                return;
            RefreshManagerInfo();
        }

        private void MenuItemExport_OnClick(object sender, RoutedEventArgs e)
        {
            DataExportWindow window = new DataExportWindow();
            window.Show();
        }

        private void MenuItemImport_OnClick(object sender, RoutedEventArgs e)
        {
            DataImportWindow window = new DataImportWindow();
            window.Show();
        }

        private void MenuItemHashCode_Click(object sender, RoutedEventArgs e)
        {
            CalHashcodeWindow window = new CalHashcodeWindow();
            window.Show();
        }

        private void MenuItemJson_OnClick(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            window.Show();
        }

        private void MenuItemTemplate_Click(object sender, RoutedEventArgs e)
        {
            CreateTemplateWindow window = new CreateTemplateWindow();
            window.Show();
        }

        private void MenuItemRebuildNpc_Click(object sender, RoutedEventArgs e)
        {
            BuildNpcDataWindow window = new BuildNpcDataWindow();
            window.ShowDialog();
        }

        private void MenuItemCheckNpc_Click(object sender, RoutedEventArgs e)
        {
            var allarenaNpc = ConfigArenanpclinkMgr.GetAll();
            foreach (var item in allarenaNpc)
            {
                var info = DicNpcMgr.GetById(item.NpcId);
                var data = NpcDataHelper.GetMemberView(info);
                item.Kpi = data.Kpi;
                ConfigArenanpclinkMgr.Update(item);
            }
        }

        private void btnGetMessage_Click(object sender, RoutedEventArgs e)
        {
            lblMessageDes.Content = "";
            if (string.IsNullOrEmpty(txtMessageCode.Text))
                return;
            int code = ConvertHelper.ConvertToInt(txtMessageCode.Text, -1000);
            if (code != -1000)
            {
                lblMessageDes.Content = EmulatorHelper.GetMessage(code);
            }
        }

        private void btnCharge_Click(object sender, RoutedEventArgs e)
        {
            ChargeWindow window = new ChargeWindow();
            window.Show();
        }

        private void MenuItemGenerateASFile_Click(object sender, RoutedEventArgs e)
        {
            GenerateASFileWindow window = new GenerateASFileWindow();
            window.Show();
        }

        private void MenuItemOnlineMatch_Click(object sender, RoutedEventArgs e)
        {
            MethodDebugWindow window = new MethodDebugWindow();
            window.Show();
        }
        #endregion

        #region Facade
        public void ResponseLog(string response,DateTime time,long cost)
        {
            ResponseLogUserControl1.AddResponse(response,time,cost);
        }

        public void RequestLog(string request,DateTime time)
        {
            ResponseLogUserControl1.AddRequest(request,time);
        }

        public void ShowMainControl(EnumWpfMainControl control)
        {
            ShowMainControl(control.ToString());
        }

        /// <summary>
        /// 比赛后返回之前的状态
        /// </summary>
        public void ShowMainControl(EnumMatchType matchType)
        {
            switch (matchType)
            {
                default:
                    GridTabMain.Children.Clear();
                    break;
            }
        }

        public void TreeSelectedHandler(string tag)
        {
            ShowMainControl(tag);
        }

        public void RefreshManagerInfo()
        {
            //var manager = WpfManagerCommand.GetManager();
            //if (manager.Code == (int)MessageCode.Success)
            //{
            //    BindManagerInfo(manager.Data.ManagerInfo);
            //}
        }

        public void UpdatePoint()
        {
            lblManagerPoint.Content = "点:" + ApiTestCore.CurPoint;
        }

        public void UpdateCoin()
        {
            lblManagerMoney.Content = "金:" + ApiTestCore.CurManager.Coin;
        }

        public void UpdateLevel()
        {
            lblManagerLevel.Content = "Lv " + ApiTestCore.CurManager.Level;
        }

        public void UpdateExp()
        {
            lblManagerExp.Content = "经验：" + ApiTestCore.CurManager.EXP + "/" + ApiTestCore.CurManager.LevelupExp;
        }

        public void UpdateStamina(int stamina)
        {
            ApiTestCore.CurManagerExtra.Stamina = stamina;
            lblManagerStamina.Content = "体力：" + string.Format("{0}/{1}", ApiTestCore.CurManagerExtra.Stamina, ApiTestCore.CurManagerExtra.StaminaMax);
            lblManagerStaminaResume.Content = "--";
        }

        public void ShowFightInfo(EnumMatchType matchType,Guid param)
        {
            //var element = new FightInfoControl(matchType, param);
            //ShowMainControl(element);
        }

        public void ShowFightInfo(EnumMatchType matchType, Guid param,Guid matchId)
        {
            //var element = new FightInfoControl(matchType, param,matchId);
            //ShowMainControl(element);
        }

        public void ShowFightInfo(EnumMatchType matchType, Guid param,int stage)
        {
            //var element = new FightInfoControl(matchType, param,stage);
            //ShowMainControl(element);
        }

        public void ShowMatchPlay(EnumMatchType matchType,Guid matchId,MatchReport match)
        {
            //var form = new MatchView(match);
            //form.ShowDialog();
            //if (form.DialogResult.HasValue && form.DialogResult == true)
            //{
            //    var resultView = new MatchResultControl(matchType,matchId);
            //    ShowMainControl(resultView);
            //}
        }

        public void ShowTourLotteryIn(Guid matchId)
        {
            //var element = new TourLotteryInControl(matchId);
            //ShowMainControl(element);
        }
        #endregion

        #region doAction
        void InitCache(object o)
        {
            try
            {
                CacheDataHelper.Instance.GetHashCode();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ApiTestCore.MainWindow.Dispatcher.Invoke((MethodInvoker) delegate
                    {
                        throw new Exception("ApiTestWindow,initcache",ex);
                    });
            }
        }

        #region BindManagerInfo()
        public void BindManagerInfo(NBManagerInfoData managerData)
        {
            ApiTestCore.CurManager = managerData.Manager;
            ApiTestCore.CurManagerExtra = managerData.ManagerExtra;
            ApiTestCore.CurPoint = managerData.Point;
            ApiTestCore.ServerTime = managerData.ServerTime;
            lblManagerName.Content = ApiTestCore.CurManager.Name;
            logoUserControl1.SetLogo(ApiTestCore.CurManager.Logo);
            UpdateLevel();
            UpdateExp();
            UpdateCoin();
            lblManagerVip.Content = "Vip" + ApiTestCore.CurManager.VipLevel;
            UpdatePoint();
            lblManagerStamina.Content ="体力："+ string.Format("{0}/{1}",ApiTestCore.CurManagerExtra.Stamina,ApiTestCore.CurManagerExtra.StaminaMax);
            lblManagerStaminaResume.Content = "恢复：" +
                                              ShareUtil.GetTime(managerData.ManagerExtra.ResumeStaminaTimeTick)
                                                       .ToString("MM-dd HH:mm:ss.fff");
            
            _serverTime = ShareUtil.GetTime(ApiTestCore.ServerTime);
            _countdownTimer.Start();
            _heartTimer.Start();
            ApiTestCore.UpdateTask();
        }
        #endregion

        #region ShowMainControl
        void ShowMainControl(string tag)
        {
            UIElement element = null;
            tag = tag.ToLower();
            switch (tag)
            {
                case "sendmoney":
                    element = new AdminUserControl();
                    break;
                case "apidebug":
                    element = new ApiUserControl();
                    break;
                case "package":
                    //element = new PackageControl();
                    break;
                case "constellationpackage":
                    //element = new ConstellationPackageControl1();
                    break;
                case "tour":
                    //element = new TourControl();
                    break;
                case "elite":
                    //element = new TourEliteControl();
                    break;
                case "ladder":
                    //element = new LadderControl();
                    break;
                case "teammember":
                    //element = new TeammemberControl();
                    break;
                case "teammembergrow":
                    //element=new TeammemberGrowControl();
                    break;
                case "formationlevelup":
                    //element = new FormationManagerControl();
                    break;
                case "teammembertrain":
                    //element = new TeammemberTrainControl();
                    break;
                case "mall":
                    //element= new MallControl();
                    break;
                case "dailycup":
                    //element=new DailycupControl();
                    break;
                case "league":
                    //element=new LeagueControl();
                    break;
                case "scouting":
                    //element=new ScoutingControl();
                    break;
                case "task":
                    //var win = new TaskForm();
                    //win.ShowDialog();
                    return;
                case "mail":
                    //element = new MailControl();
                    break;
                case "chat":
                    //var win1 = new ChatWindow();
                    //win1.Show();
                    return;
                case "wchrecord":
                    //element = new WorldChallengeRecordControl();
                    break;
                case "wchinfo":
                    //element = new WorldChallengeControl();
                    break;
                case "auction":
                    //element = new AuctionControl();
                    break;
                case "tourauto":
                    //element = new TourAutoControl();
                    break;
                case "arena":
                    //element = new ArenaControl();
                    break;
                case "crowd":
                    //element = new CrowdControl();
                    break;
            }
            if(element!=null)
                ShowMainControl(element);
        }

        private void ShowMainControl(UIElement element)
        {
            GridTabMain.Children.Clear();
            GridTabMain.Children.Add(element);
            TabMain.SelectedItem = TabItemMain;
        }

        #endregion

        #region ShowPanels
        void ShowPanels(ShowPanelType type)
        {
            CanvasMain.Visibility = Visibility.Hidden;
            GridLogin.Visibility = Visibility.Hidden;
            GridRegister.Visibility = Visibility.Hidden;
            switch (type)
            {
                case ShowPanelType.Login:
                    GridLogin.Visibility = Visibility.Visible;
                    txtLoginAccount.Focus();
                    break;
                case ShowPanelType.Register:
                    GridRegister.Visibility = Visibility.Visible;
                    break;
                case ShowPanelType.Main:
                    CanvasMain.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region ShowMain
        void ShowMain(bool showRegister)
        {
            //检查是否注册
            var manager = WpfManagerCommand.GetManager();
            if (manager.Code == (int)MessageCode.Success)
            {
                _isLogin = true;
                BindManagerInfo(manager.Data);
                ShowPanels(ShowPanelType.Main);
            }
            else if (showRegister == true && (manager.Code == (int)MessageCode.LoginNoRegister || manager.Code == (int)MessageCode.LoginNoUser))
            {
                ShowPanels(ShowPanelType.Register);
            }
            else
            {
                MessageBox.Show("获取经理信息失败，code：" + manager.Code);
            }
        }
        #endregion
        #endregion

        #region Login
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string account = txtLoginAccount.Text;
            if (Check(account))
            {
                //var response = RequestHelper.RequestPassport(account);
                string name = txtLoginAccount.Text; ;
                if (string.IsNullOrEmpty(name))
                    MessageBox.Show("经理名不能为空");
                var response = WpfManagerCommand.Login(name);
                //WpfReturnMessage message = JsonConvert.DeserializeObject<WpfReturnMessage>(response);
                if (response.Code != 0)
                {
                    if (response.Code == (int) MessageCode.LoginNoRegister ||
                        response.Code == (int) MessageCode.LoginNoUser)
                    {
                        ShowPanels(ShowPanelType.Register);
                    }
                    else
                        ResponseMessage(response.Cookie);
                }
                else
                {
                    RequestHelper._cookie = response.Cookie;
                    ShowMain(true);
                }
            }
        }

        private bool Check(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ResponseMessage("用户名不能为空");
                return false;
            }
            //去掉空格和控制字符，不然MemcachedClient会抛异常
            if (username.Contains(" ") || username.Contains("\n") || username.Contains("\r") || username.Contains("\t") || username.Contains("\f") || username.Contains("\v"))
            {
                ResponseMessage("用户名包含无效字符，请重新输入");
                return false;
            }
            return true;
        }

        private void ResponseMessage(string message)
        {
            lblLoginMessage.Content = message;
        }
        #endregion

        #region Register
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = txtRegisterName.Text;
            string area = cmbRegisterArea.Text;
            string logo = cmbRegisterLogo.Text;
            int templateId = 1;
            if (string.IsNullOrEmpty(name))
                MessageBox.Show("经理名不能为空");
            var response = WpfManagerCommand.Register(name, area, logo, templateId);
            if (response.Code == (int)MessageCode.Success)
            {
                ShowMain(false);
            }
            else
            {
                MessageBox.Show("注册失败，code：" + response.Code);
            }
        }
        #endregion

        void UpdateTime(object sender, EventArgs e)
        {
            _serverTime = _serverTime.AddSeconds(1);
            lblServerTime.Content = _serverTime.ToString("yyyy-MM-dd HH:mm:ss");
            lblServerTimeTick.Content = ShareUtil.GetTimeTick(_serverTime);
        }

        void Heart(object sender, EventArgs e)
        {
            WpfManagerCommand.Heart();
        }

        

        
    }
}
