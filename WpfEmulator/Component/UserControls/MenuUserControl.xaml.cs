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

namespace Games.NBall.WpfEmulator.Component.UserControls
{
    /// <summary>
    /// MenuUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {
            InitializeComponent();
        }


        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private void MenuItemCreateBot_OnClick(object sender, RoutedEventArgs e)
        {
            CreateBotWindow createBotWindow = new CreateBotWindow();
            createBotWindow.Show();
        }

        private void MenuItemRefreshManager_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
