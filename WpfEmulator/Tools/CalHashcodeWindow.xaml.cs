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
using Games.NBall.Bll.Share;
using Games.NBall.WpfEmulator.Core;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// CalHashcodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalHashcodeWindow : Window
    {
        public CalHashcodeWindow()
        {
            InitializeComponent();
            if (ApiTestCore.CurManager != null)
            {
                txtGuid.Text = ApiTestCore.CurManager.Idx.ToString();
            }
        }

        private void btnCalHashcode_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtGuid.Text))
            {
                lblResult.Content = "请输入要计算的guid";
                return;
            }
            try
            {

                lblResult.Content = "结果：" + ShareUtil.GetTableMod(new Guid(txtGuid.Text));
            }
            catch (Exception ex)
            {
                lblResult.Content = "计算出错：" + ex.Message;
            }
        }
    }
}
