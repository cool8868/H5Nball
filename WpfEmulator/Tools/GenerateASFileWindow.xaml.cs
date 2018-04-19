using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Games.NBall.WpfEmulator.Core;
using Games.NBall.WpfEmulator.Entity;
using System.Reflection;

namespace Games.NBall.WpfEmulator.Tools
{
    /// <summary>
    /// GenerateASFileWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateASFileWindow : Window
    {
        public GenerateASFileWindow()
        {
            InitializeComponent();
            InitData();
            FillFileList();
        }

        /// <summary>
        /// _dicType存放文件名--实体类型
        /// </summary>
        private Dictionary<string, Type> _dicType = new Dictionary<string, Type>();

        private List<string> _choosedTypeList = new List<string>();

        public ObservableCollection<DataItem> _dataItems = new ObservableCollection<DataItem>();

        private void InitData()
        {
            //_dicType.Add("RequestConfig", typeof (RequestConfigEntity));
            //_dicType.Add("RequestConfig_Debug", typeof (RequestConfigEntity));
            //_dicType.Add("Message", typeof (MessageConfigEntity));
            //_dicType.Add("ItemTip", typeof (ItemTipsEntity));
            //_dicType.Add("SkillTip", typeof (SkillTipsEntity));
            //_dicType.Add("Formation", typeof (FormationConfigEntity));
            //_dicType.Add("DescriptionDic", typeof (DescriptionConfigEntity));
            //_dicType.Add("NameLibrary", typeof (NameLibraryEntity));
            //_dicType.Add("NpcDic", typeof (NpcDicEntity));       
        }
        
        private void FillFileList()
        {
            List<string> fileNames = new List<string>();
            foreach (var name in _dicType)
            {
                fileNames.Add(name.Key);
            }
            CBoxFiles.ItemsSource = fileNames;
            CBoxFiles.SelectedIndex = 0;
        }


        private void btnGenerateAS_Click(object sender, RoutedEventArgs e)
        {
            GetDataGridItems();
            Type entityType = GenerateASFile.GetPropertyType(_dicType[CBoxFiles.SelectedItem.ToString()], CboxKeyList.SelectedItem.ToString());

            if (null != entityType)
            {
                var properties = GenerateASFile.GetPropertyInfo(entityType);
                GenerateASFile.CreateFileDbData(properties, CboxKeyList.SelectedItem.ToString(), _choosedTypeList);
                GenerateASFile.CreateFileDb(properties, CboxKeyList.SelectedItem.ToString(), TxtParams.Text);
            }

        }

        private void cboxKeyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //根据选择的属性名获得泛型实参的属性
            _dataItems.Clear();
            if (CboxKeyList.SelectedIndex >= 0)
            {
                Type entityType = GenerateASFile.GetPropertyType(_dicType[CBoxFiles.SelectedItem.ToString()], CboxKeyList.SelectedItem.ToString());
                if (null == entityType)
                {
                    return;
                }
                var properties = GenerateASFile.GetPropertyInfo(entityType);
                foreach (var pro in properties)
                {
                    DataItem member = new DataItem(pro.Name, EnumASType.Int);
                    _dataItems.Add(member);
                    DataGridParamList.DataContext = _dataItems;
                }
            }
        
        }

        private void CBoxFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {      
            PropertyInfo[] properties = GenerateASFile.GetPropertyInfo(_dicType[CBoxFiles.SelectedItem.ToString()]);
            List<string> _keyList = new List<string>();//实体类型所有的方法名
            foreach (var pro in properties)
            {
                _keyList.Add(pro.Name);
            }
            CboxKeyList.ItemsSource = _keyList;
                    
        }

        

        private void GetDataGridItems()
        {
 
            for (int i = 0; i < DataGridParamList.Items.Count; i++)
            {               
                ComboBox type = DataGridParamList.Columns[1].GetCellContent(DataGridParamList.Items[i]) as ComboBox;
                if (null != type)
                {
                    string value = type.Text;
                    if (value == "Int")
                    {
                        value = value.ToLower();
                    }
                    _choosedTypeList.Add(value);
                }
                
            }
        }
    }

    /// <summary>
    /// DataGrid数据项
    /// </summary>
    public class DataItem
    {
        public DataItem(string name, EnumASType typeEnum)
        {
            MemberName = name;
            MemberType = typeEnum;
        }
        public string MemberName { get; set; }
        public EnumASType MemberType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumASType
    {
        Int,
        String,
        Array,
        Number
    }

}
