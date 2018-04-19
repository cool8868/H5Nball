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
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Core;

namespace Games.NBall.WpfEmulator.Component.Forms
{
    /// <summary>
    /// ChargeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChargeWindow : Window
    {
        public ChargeWindow()
        {
            InitializeComponent();
            dpChargeDate.SelectedDate = DateTime.Now;
        }
        private void btnChargeTest_Click(object sender, RoutedEventArgs e)
        {
            int point = Common.ConvertHelper.ConvertToInt(txtPoint.Text);
            if (point % 10 != 0 || point <= 0)
            {
                MessageBox.Show("充值点券数必须为10的倍数。");
            }
            else
            {
                var time = Convert.ToDateTime(dpChargeDate.Text);
                time = time.Date.AddHours(3);
                var response = AdminHelper.ChargeTest(point, time);
                if (response == (int)MessageCode.Success)
                {
                    MessageBox.Show("充值成功!");

                    ApiTestCore.MainWindow.RefreshManagerInfo();
                }
                else
                {
                    MessageBox.Show(EmulatorHelper.BuildErrorinfo("充值", response));
                }
            }

        }
        private void btnCharge_Click(object sender, RoutedEventArgs e)
        {
            int point = Common.ConvertHelper.ConvertToInt(txtPoint.Text);
            if (point%10 != 0 || point<=0)
            {
                MessageBox.Show("充值点券数必须为10的倍数。");
            }
            else
            {
                var time = Convert.ToDateTime(dpChargeDate.Text);
                time = time.Date.AddHours(3);
                var response = new AdminHelper().Charge(point,time);
                if (response == (int)MessageCode.Success)
                {
                        MessageBox.Show("充值成功!");
                    
                    ApiTestCore.MainWindow.RefreshManagerInfo();
                }
                else
                {
                    MessageBox.Show(EmulatorHelper.BuildErrorinfo("充值", response));
                }
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
