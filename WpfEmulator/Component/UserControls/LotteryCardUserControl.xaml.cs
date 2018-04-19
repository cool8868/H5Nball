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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Component.UserControls
{
    /// <summary>
    /// LotteryCardUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LotteryCardUserControl : UserControl
    {
        private int _itemCode;
        public LotteryCardUserControl()
        {
            InitializeComponent();
        }

        public void SetData(string itemCode)
        {
            
        }

        public void ShowCard()
        {
            lblStatus.Visibility = Visibility.Hidden;
            lblStatus.Content = "";
            recFront.Visibility = Visibility.Visible;
            lblName.Visibility = Visibility.Visible;

        }

        public void HideCard()
        {
            _itemCode = 0;
            lblName.Content = "";
            lblStatus.Visibility = Visibility.Hidden;
            lblStatus.Content = "";
            recFront.Visibility = Visibility.Hidden;
            lblName.Visibility = Visibility.Hidden;

        }

        public void ShowCardGet(bool isGet)
        {
            lblStatus.Visibility = Visibility.Visible;
            if (isGet)
                lblStatus.Content = "已获取";
            else
            {
                lblStatus.Content = "未获取";
            }
        }

    }
}
