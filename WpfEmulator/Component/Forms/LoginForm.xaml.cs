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
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;
using Newtonsoft.Json;

namespace Games.NBall.WpfEmulator.Component.Forms
{
    /// <summary>
    /// LoginForm.xaml 的交互逻辑
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            AccountTextBox.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
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
            MessageLabel.Content = message;
        }

    }
}
