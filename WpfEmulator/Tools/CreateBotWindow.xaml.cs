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
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Core;

namespace Games.NBall.WpfEmulator
{
    /// <summary>
    /// ToolsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateBotWindow : Window
    {
        public CreateBotWindow()
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
            for (int i=0;i<count;i++)
            {
                try
                {
                    string fullName = pre + i;

                    var response = ManagerCore.Instance.CreateManager(fullName);
                    if (response.Code == (int) MessageCode.Success)
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("创建失败",EmulatorHelper.BuildErrorinfo(response.Code));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("创建失败:" + ex.Message);
                    return;
                }
            }
            MessageBox.Show("创建成功.");
        }
    }
}
