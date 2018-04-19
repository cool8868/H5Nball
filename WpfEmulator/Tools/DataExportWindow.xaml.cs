using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
using Games.NBall.Common;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;
using Newtonsoft.Json;
using System.IO;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// ExportMessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataExportWindow : Window
    {
        public DataExportWindow()
        {
            InitializeComponent();
        }

        private readonly string path = AppDomain.CurrentDomain.BaseDirectory + "ClientData\\";

        private void btnExportMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var message = DataExport.ExportMesseageConfig();
                var path = EmulatorHelper.SaveConfig<MessageConfigEntity>(message, EmulatorHelper.MessageFileName);
                MessageBox.Show("导出成功，目录：" + path);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                MessageBox.Show("出错了：" + ex.Message);
            }
        }

        private void btnExportItem_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportItemTips();
            var path = EmulatorHelper.SaveConfig<ItemTipsEntity>(config, EmulatorHelper.ItemtipFileName);
            CacheDataHelper.Instance.RefreshItemtips(config);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportSkill_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportSkillTips();
            var path = EmulatorHelper.SaveConfig<SkillTipsEntity>(config, EmulatorHelper.SkilltipFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }
        private void btnExportSkill2_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportSkillTips(true);
            var path = EmulatorHelper.SaveConfig<SkillTipsEntity>(config, EmulatorHelper.SkilltipFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportApi_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportRequestConfig(false);
            var path = EmulatorHelper.SaveConfig<RequestConfigEntity>(config, EmulatorHelper.RequestConfigFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportApiDebug_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportRequestConfig(true);
            var path = EmulatorHelper.SaveConfig<RequestConfigEntity>(config, EmulatorHelper.RequestConfigDebugFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportFormation_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportFormationConfig();
            var path = EmulatorHelper.SaveConfig<FormationConfigEntity>(config, EmulatorHelper.FormationFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportDescription_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportDescriptionDic();
            var path = EmulatorHelper.SaveConfig<DescriptionConfigEntity>(config, EmulatorHelper.DescriptionDicFileName);
            Type tp = typeof(DescriptionConfigEntity);
            dynamic t = Activator.CreateInstance(tp);
            DataExport.ActivatorCreateType(tp, t);
            var summary = DataExport.BuildSummaryDic(Assembly.GetExecutingAssembly(), "Games.NBall.WpfEmulator.XML");
            EmulatorHelper.SaveConfig<DescriptionConfigEntity>(t, EmulatorHelper.DescriptionDicDebugFileName, summary);
            //MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportDescription1_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportDescriptionDic1();
            var path = EmulatorHelper.SaveConfig<DescriptionConfigEntity1>(config, EmulatorHelper.DescriptionDicFileName1);
            MessageBox.Show("导出成功，目录：" + path);
        }
        
        private void btnExportPlaryKpi_Click(object sender, RoutedEventArgs e)
        {
            //var config = DataExport.ExportPlaryKpi();
            //var path = EmulatorHelper.SaveConfig<DescriptionConfigPlayerKpiEntity>(config, EmulatorHelper.DescriptionPlayerKpi);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportNameLibrary_Click(object sender, RoutedEventArgs e)
        {
            var config = DataExport.ExportNameLibrary();
            var path = EmulatorHelper.SaveConfig<NameLibraryEntity>(config, EmulatorHelper.NameLibraryFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportNpcConfig_Click(object sender, RoutedEventArgs e)
        {
           // var config = DataExport.ExportNpcConfig();
            //var path = EmulatorHelper.SaveConfig<NpcDicEntity>(config, EmulatorHelper.NpcDicFileName);
            MessageBox.Show("导出成功，目录：" + path);
        }

        private void btnExportAll_Click(object sender, RoutedEventArgs e)
        {
            //var message = DataExport.ExportMesseageConfig();
            //EmulatorHelper.SaveConfig<MessageConfigEntity>(message, EmulatorHelper.MessageFileName);
            
            //var config = DataExport.ExportItemTips();
            //EmulatorHelper.SaveConfig<ItemTipsEntity>(config, EmulatorHelper.ItemtipFileName);

            //var config1 = DataExport.ExportSkillTips();
            //EmulatorHelper.SaveConfig<SkillTipsEntity>(config1, EmulatorHelper.SkilltipFileName);

            //var config2 = DataExport.ExportRequestConfig(false);
            //EmulatorHelper.SaveConfig<RequestConfigEntity>(config2, EmulatorHelper.RequestConfigFileName);

            //var config3 = DataExport.ExportRequestConfig(true);
            //EmulatorHelper.SaveConfig<RequestConfigEntity>(config3, EmulatorHelper.RequestConfigDebugFileName);

            //var config4 = DataExport.ExportFormationConfig();
            //EmulatorHelper.SaveConfig<FormationConfigEntity>(config4, EmulatorHelper.FormationFileName);

            //var config5 = DataExport.ExportDescriptionDic();
            //EmulatorHelper.SaveConfig<DescriptionConfigEntity>(config5, EmulatorHelper.DescriptionDicFileName);

            //var config6 = DataExport.ExportNameLibrary();
            //EmulatorHelper.SaveConfig<NameLibraryEntity>(config6, EmulatorHelper.NameLibraryFileName);

            //var config7 = DataExport.ExportNpcConfig();
            //EmulatorHelper.SaveConfig<NpcDicEntity>(config7, EmulatorHelper.NpcDicFileName);

            //var config8 = DataExport.ExportDescriptionDic1();
            //EmulatorHelper.SaveConfig<DescriptionConfig1Entity>(config8, EmulatorHelper.DescriptionDicFileName1);
            //MessageBox.Show("导出成功!");
        }

        private bool showJson(JsonFormatWindow window, string fileName)
        {
            if (File.Exists(path + fileName))
            {  
                using (StreamReader reader = new StreamReader(path + fileName))
                {
                    string s = reader.ReadToEnd();
                    window.jsonViewer1.Json = s;
                    return true;
                }
            }
            else
            {
                MessageBox.Show("先导出");
                return false;
            }
            
        }

        private void btnApiCheck_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.RequestConfigFileName))
                window.Show();
        }

        private void btnApiDebug_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.RequestConfigDebugFileName))
                window.Show();
        }

        private void btnEnumMessage_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.MessageFileName))
                window.Show();
        }

        private void btnItemTips_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.ItemtipFileName))
                window.Show();
        }

        private void btnSkillTips_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.SkilltipFileName))
                window.Show();
        }

        private void btnFormation_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.FormationFileName))
                window.Show();
        }

        private void btnDescriptionDic_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.DescriptionDicFileName))
                window.Show();
        }

        private void btnDescriptionDic1_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if (showJson(window, EmulatorHelper.DescriptionDicFileName1))
                window.Show();
        }


        private void btnPlaryKpi_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if (showJson(window, EmulatorHelper.DescriptionPlayerKpi))
                window.Show();
        }

        private void btnRandomName_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.NameLibraryFileName))
                window.Show();
        }

        private void btnNpcDic_Click(object sender, RoutedEventArgs e)
        {
            JsonFormatWindow window = new JsonFormatWindow();
            if(showJson(window, EmulatorHelper.NpcDicFileName))
                window.Show();
        }


        
    }
}
