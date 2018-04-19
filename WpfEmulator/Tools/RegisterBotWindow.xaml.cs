using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Core;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// RegisterBotWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterBotWindow : Window
    {
        public RegisterBotWindow()
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
            if (count > 10000)
            {
                MessageBox.Show("一次最多创建一万个.");
                return;
            }


            if (RegisterGambleAccount() == false)
                return;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    string fullName = pre + i;
                    ManagerCore.Instance.GetUserByAccount(fullName, "emulator",1);

                    var response = ManagerCore.Instance.RegisterManager( fullName,fullName,"1",1,true,"0:0:0:0");
                    if (response.Code == (int)MessageCode.Success)
                    {
                      //  LadderCore.Instance.GetLadderManager(response.Data);
                    }
                    else
                    {
                        MessageBox.Show("注册失败", EmulatorHelper.BuildErrorinfo(response.Code));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Insert(ex);
                    MessageBox.Show("注册失败:" + ex.Message);
                    return;
                }
            }

            MessageBox.Show("注册成功.");
        }

        bool RegisterGambleAccount()
        {
            var manager = ManagerCore.Instance.GetManager(EmulatorHelper.TestAccount);
            if (manager == null)
            {
                ManagerCore.Instance.GetUserByAccount(EmulatorHelper.TestAccount, "000");
                var response = ManagerCore.Instance.RegisterManager(EmulatorHelper.TestAccount, "EmulTest01", "1", 1,"0:0:0:0");
                if (response.Code == (int) MessageCode.Success)
                {
                    var payUser = PayUserMgr.GetById(EmulatorHelper.TestAccount);
                    if (payUser == null)
                    {
                        payUser=new PayUserEntity();
                        payUser.Account = EmulatorHelper.TestAccount;
                        payUser.RowTime = DateTime.Now;
                        PayUserMgr.Insert(payUser);
                    }
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
    }
}
