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
using Games.NBall.WpfEmulator.Command;

namespace Games.NBall.WpfEmulator.Component.Forms
{
    /// <summary>
    /// PackageForm.xaml 的交互逻辑
    /// </summary>
    public partial class PackageForm : Window
    {
        public PackageForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var package = WpfItemCommand.GetPackage();
            if (package != null)
            {
                lblGridMemo.Content =string.Format("格数:{0}/{1}",package.Data.Items.Count, package.Data.PackageSize);
                DataGridPackage.ItemsSource = package.Data.Items;
            }
        }
    }
}
