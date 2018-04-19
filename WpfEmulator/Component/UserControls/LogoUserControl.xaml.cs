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
using Games.NBall.Common;

namespace Games.NBall.WpfEmulator.Component.UserControls
{
    /// <summary>
    /// LogoUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogoUserControl : UserControl
    {
        public LogoUserControl()
        {
            InitializeComponent();
        }

        public void SetLogo(string logo)
        {
            int logoInt = ConvertHelper.ConvertToInt(logo);
            SetLogo(logoInt);
        }


        public void SetLogo(int logoInt)
        {
            
            if (logoInt > 0 && logoInt < 30)
                imageLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Logo/" + logoInt + ".png"));
        }
    }
}
