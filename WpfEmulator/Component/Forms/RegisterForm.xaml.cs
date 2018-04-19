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
using Games.NBall.WpfEmulator.Command;

namespace Games.NBall.WpfEmulator.Component.Forms
{
    /// <summary>
    /// RegisterForm.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string area = AreaComboBox.Text;
            if (string.IsNullOrEmpty(name))
                MessageBox.Show("经理名不能为空");
            var response = WpfManagerCommand.Register(name, area, "1",1);
            if (response.Code == (int) MessageCode.Success)
            {
                this.DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("注册失败，code："+response.Code);
            }
        }
    }
}
