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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Core;

namespace Games.NBall.WpfEmulator
{
    /// <summary>
    /// ServerTool.xaml 的交互逻辑
    /// </summary>
    public partial class ServerTool : Window
    {
        public ServerTool()
        {
            InitializeComponent();
        }

        private void btnCreateBot_Click(object sender, RoutedEventArgs e)
        {
            string pre = txtBotPrev.Text;
            string countStr = txtBotCount.Text;
            if (string.IsNullOrEmpty(pre) || string.IsNullOrEmpty(countStr))
            {
                MessageBox.Show("请输入参数！");
                return;
            }
            int count = ConvertHelper.ConvertToInt(countStr, 1);
            if (count > 100000)
            {
                MessageBox.Show("一次最多创建10万个.");
                return;
            }
            btnCreateBot.IsEnabled = false;
            btnCreateBot2.IsEnabled = false;
            _totalCount = count;
            _finishCount = 0;
            progress1.Maximum = _totalCount;
            progress1.Value = 0;
            lblProgress.Content = string.Format("{0}/{1}", 0, _totalCount);
            _thread = new Thread(() => StartCreateBot(_totalCount,pre, CreateCallback, FinishCallback));
            _thread.IsBackground = true;
            _thread.Start();
        }
        private Thread _thread;
        private int _finishCount = 0;
        private int _totalCount = 0;
        public delegate void CreateDelegate(string message);
        public delegate void FinishDelegate();
        public void StartCreateBot(int totalCount,string pre, CreateDelegate createDelegate,FinishDelegate finishDelegate)
        {
            for (int i = 0; i < totalCount; i++)
            {
                try
                {
                    string fullName = pre + i;

                    var response = ManagerCore.Instance.CreateManager(fullName);
                    if (response.Code == (int)MessageCode.Success)
                    {
                        Dispatcher.Invoke((Action)delegate { createDelegate(""); });
                    }
                    else
                    {
                        Dispatcher.Invoke((Action)delegate { createDelegate(EmulatorHelper.BuildErrorinfo(response.Code)); });
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex);
                    Dispatcher.Invoke((Action)delegate { createDelegate(ex.Message); });
                    return;
                }
            }
            Dispatcher.Invoke((Action)delegate { finishDelegate(); });
        }

        private void btnCreateBot2_Click(object sender, RoutedEventArgs e)
        {
            string pre = txtBotPrev2.Text;
            string countStr = txtBotCount2.Text;
            if (string.IsNullOrEmpty(pre) || string.IsNullOrEmpty(countStr))
            {
                MessageBox.Show("请输入参数！");
                return;
            }
            int count = ConvertHelper.ConvertToInt(countStr, 1);
            if (count > 10000)
            {
                MessageBox.Show("一次最多创建一万个.");
                return;
            }


            if (RegisterGambleAccount() == false)
                return;
            btnCreateBot.IsEnabled = false;
            btnCreateBot2.IsEnabled = false;
            _totalCount = count;
            _finishCount = 0;
            progress1.Maximum = _totalCount;
            progress1.Value = 0;
            lblProgress.Content = string.Format("{0}/{1}", 0, _totalCount);

            _thread = new Thread(() => StartCreateBot2(count, pre, CreateCallback, FinishCallback));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void StartCreateBot2(int totalCount, string pre, CreateDelegate createDelegate,
                                   FinishDelegate finishDelegate)
        {
            for (int i = 0; i < totalCount; i++)
            {
                try
                {
                    string fullName = pre + i;
                    ManagerCore.Instance.GetUserByAccount(fullName, "emulator", 1);
                    int tempid = RandomHelper.GetInt32(1, 8);
                    var response = ManagerCore.Instance.RegisterManager(fullName, fullName, "1", tempid, true, "0:0:0:0");
                    if (response.Code == (int)MessageCode.Success)
                    {
                        //LadderCore.Instance.GetLadderManager(response.Data);
                        Dispatcher.Invoke((Action)delegate { createDelegate(""); });
                    }
                    else
                    {
                        Dispatcher.Invoke((Action)delegate { createDelegate(EmulatorHelper.BuildErrorinfo(response.Code)); });
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex);
                    Dispatcher.Invoke((Action)delegate { createDelegate(ex.Message); });
                    return;
                }
            }
            Dispatcher.Invoke((Action)delegate { finishDelegate(); });
        }

        bool RegisterGambleAccount()
        {
            var manager = ManagerCore.Instance.GetManager(EmulatorHelper.TestAccount);
            if (manager == null)
            {
                ManagerCore.Instance.GetUserByAccount(EmulatorHelper.TestAccount, "000");
                var response = ManagerCore.Instance.RegisterManager(EmulatorHelper.TestAccount, "EmulTest01", "1", 1, true, "0:0:0:0");
                var entity =PayUserMgr.GetById(EmulatorHelper.TestAccount);
                if (entity == null)
                {
                    entity = new PayUserEntity();
                    entity.Account = EmulatorHelper.TestAccount;
                    entity.RowTime = DateTime.Now;
                    PayUserMgr.Insert(entity);
                }

                if (response.Code == (int)MessageCode.Success)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("注册失败", EmulatorHelper.BuildErrorinfo(response.Code));
                    return false;
                }
            }
            return true;
        }

        public void CreateCallback(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                btnCreateBot.IsEnabled = true;
                btnCreateBot2.IsEnabled = true;
                lblProgress.Content = message;
            }
            else
            {
                _finishCount++;
                progress1.Value = _finishCount;
                lblProgress.Content = string.Format("{0}/{1}", _finishCount, _totalCount);
            }
        }

        public void FinishCallback()
        {
            lblProgress.Content = "生成成功!";
            btnCreateBot.IsEnabled = true;
            btnCreateBot2.IsEnabled = true;
        }
    }
}
