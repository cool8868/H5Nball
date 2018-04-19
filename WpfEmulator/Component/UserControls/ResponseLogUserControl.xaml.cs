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

namespace Games.NBall.WpfEmulator.Component
{
    /// <summary>
    /// ResponseLogUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ResponseLogUserControl : UserControl
    {
        public ResponseLogUserControl()
        {
            InitializeComponent();
            CommandBinding cb = new CommandBinding(ApplicationCommands.Copy, CopyCmdExecuted, CopyCmdCanExecute);

            this.LogListBox.CommandBindings.Add(cb);  
        }

        public void AddResponse(string s,DateTime time,long cost)
        {
            LogListBox.Items.Add(string.Format("{0:yyyy-dd-MM HH:mm:ss,fff} [Response](cost {1}ms):", time,cost));
            LogListBox.Items.Add(s);
            LogListBox.Items.Add("");
        }

        public void AddRequest(string s,DateTime time)
        {
            LogListBox.Items.Add(string.Format("{0:yyyy-dd-MM HH:mm:ss,fff} [Request]:", time));
            LogListBox.Items.Add( s);
        }

        void CopyCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;
            // The SelectedItems could be ListBoxItem instances or data bound objects depending on how you populate the ListBox.  
            Clipboard.SetText(lb.SelectedItem.ToString());
        }
        void CopyCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;
            // CanExecute only if there is one or more selected Item.  
            if (lb.SelectedItems.Count > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void LogListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var s = LogListBox.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(s) && s.StartsWith("{"))
                {
                    jsonViewer1.Json = s;
                }
            }
            catch
            {
            }
        }  
    }
}
