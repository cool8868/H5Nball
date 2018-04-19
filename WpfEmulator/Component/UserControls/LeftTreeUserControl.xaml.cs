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
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Component
{
    /// <summary>
    /// LeftTreeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LeftTreeUserControl : UserControl
    {
        public LeftTreeUserControl()
        {
            InitializeComponent();
        }

        private void TreeRequest_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var view = TreeRequest.SelectedItem as TreeViewItem;

            if (view != null && view.Tag != null)
            {
                string tag = view.Tag.ToString();
                ApiTestCore.MainWindow.TreeSelectedHandler(tag);
            }
        }
    }
}
