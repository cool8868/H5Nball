using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Games.NBall.Entity;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Core
{
    public class ComboBoxHelper
    {
        #region BindItem
        public static void BindItemType(ComboBox cmb)
        {
            var list = new List<KeyValueComboBoxItem>(5);

            list.Add(new KeyValueComboBoxItem(1, "球员卡"));
            list.Add(new KeyValueComboBoxItem(2, "装备"));
            list.Add(new KeyValueComboBoxItem(3, "商城道具"));
            list.Add(new KeyValueComboBoxItem(4, "球魂"));
            list.Add(new KeyValueComboBoxItem(5, "套装图纸"));
            list.Add(new KeyValueComboBoxItem(6, "徽章"));
            BindComboBox(cmb, list);
        }

        public static void BindItemSubType(ComboBox cmb, int type, out string typeName)
        {
            typeName = "--";
            var list = new List<KeyValueComboBoxItem>();
            switch (type)
            {
                case 1:
                    list.Add(new KeyValueComboBoxItem(1, "金"));
                    list.Add(new KeyValueComboBoxItem(2, "橙"));
                    list.Add(new KeyValueComboBoxItem(3, "紫"));
                    list.Add(new KeyValueComboBoxItem(4, "蓝"));
                    list.Add(new KeyValueComboBoxItem(5, "绿"));
                    typeName = "卡牌颜色";
                    break;
                case 2:
                    list.Add(new KeyValueComboBoxItem(1, "七件套"));
                    list.Add(new KeyValueComboBoxItem(2, "五件套"));
                    list.Add(new KeyValueComboBoxItem(3, "三件套"));
                    list.Add(new KeyValueComboBoxItem(4, "散件"));
                    typeName = "套装类型";
                    break;
                case 4:
                    list.Add(new KeyValueComboBoxItem(1, "彩色"));
                    list.Add(new KeyValueComboBoxItem(2, "红色"));
                    list.Add(new KeyValueComboBoxItem(3, "蓝色"));
                    list.Add(new KeyValueComboBoxItem(4, "黄色"));
                    typeName = "球魂颜色";
                    break;
                default:
                    list.Add(new KeyValueComboBoxItem(0, "所有"));
                    break;
            }

            BindComboBox(cmb, list);
        }

        public static void BindItemThirdType(ComboBox cmb, int type, out string typeName)
        {
            typeName = "--";
            var list = new List<KeyValueComboBoxItem>();
            switch (type)
            {
                case 1:
                    list.Add(new KeyValueComboBoxItem(0, "所有"));
                    list.Add(new KeyValueComboBoxItem(1, "英超联赛"));
                    list.Add(new KeyValueComboBoxItem(2, "西甲联赛"));
                    list.Add(new KeyValueComboBoxItem(3, "德甲联赛"));
                    list.Add(new KeyValueComboBoxItem(4, "意甲联赛"));
                    list.Add(new KeyValueComboBoxItem(5, "法甲联赛"));
                    typeName = "联赛级别";
                    break;
                case 2:
                    list.Add(new KeyValueComboBoxItem(0, "所有"));
                    list.Add(new KeyValueComboBoxItem(1, "史诗"));
                    list.Add(new KeyValueComboBoxItem(2, "精良"));
                    list.Add(new KeyValueComboBoxItem(3, "优质"));
                    list.Add(new KeyValueComboBoxItem(4, "普通"));
                    list.Add(new KeyValueComboBoxItem(5, "劣质"));
                    typeName = "装备品质";
                    break;
                case 4:
                    list.Add(new KeyValueComboBoxItem(0, "所有"));
                    list.Add(new KeyValueComboBoxItem(1, "1"));
                    list.Add(new KeyValueComboBoxItem(2, "2"));
                    list.Add(new KeyValueComboBoxItem(3, "3"));
                    list.Add(new KeyValueComboBoxItem(4, "4"));
                    list.Add(new KeyValueComboBoxItem(4, "5"));
                    typeName = "球魂等级";
                    break;
                default:
                    list.Add(new KeyValueComboBoxItem(0, "所有"));
                    break;
            }
            BindComboBox(cmb, list);
        }

        public static void BindChatType(ComboBox cmb)
        {
            var list = new List<KeyValueComboBoxItem>(2);

            list.Add(new KeyValueComboBoxItem(1, "世界"));
            list.Add(new KeyValueComboBoxItem(2, "私聊"));
            BindComboBox(cmb, list);
        }

        public static void BindItem(ComboBox cmb, List<DicItemEntity> itemDic)
        {
            var list = new List<KeyValueComboBoxItem>(itemDic.Count);
            foreach (var dicItem in itemDic)
            {
                list.Add(new KeyValueComboBoxItem(dicItem.ItemCode, dicItem.ItemName));
            }
            BindComboBox(cmb, list);
        }
        #endregion

        #region BindNpcType
        public static void BindNpcType(ComboBox cmb)
        {
            var list = new List<KeyValueComboBoxItem>(3);
            list.Add(new KeyValueComboBoxItem(0, "所有"));
            list.Add(new KeyValueComboBoxItem(1, "巡回赛"));
            list.Add(new KeyValueComboBoxItem(2, "精英巡回赛"));
            list.Add(new KeyValueComboBoxItem(3, "世界挑战赛"));
            list.Add(new KeyValueComboBoxItem(4, "球星启示录"));
            list.Add(new KeyValueComboBoxItem(5, "豪门试炼"));
            list.Add(new KeyValueComboBoxItem(6, "黄道十二宫"));
            list.Add(new KeyValueComboBoxItem(7, "巅峰之战"));
            BindComboBox(cmb, list);
        }

        #endregion

        public static void BindComboBox(ComboBox cmb, List<KeyValueComboBoxItem> list)
        {
            cmb.ItemsSource = list;
            //cmb.SelectedIndex = 0;
        }

        public static int GetSelectValueInt(ComboBox cmb)
        {
            string temp = GetSelectValue(cmb);
            if (string.IsNullOrEmpty(temp))
                return 0;
            else
            {
                return Convert.ToInt32(temp);
            }
        }

        public static string GetSelectValue(ComboBox cmb)
        {
            if (cmb.SelectedIndex < 0)
                return "";
            else
            {
                var temp = cmb.SelectedItem as KeyValueComboBoxItem;
                return temp.Value.ToString();
            }
        }

        public static void SetSelectItem(ComboBox cmb, int value)
        {
            SetSelectItem(cmb, value.ToString());
        }

        public static void SetSelectItem(ComboBox cmb, string value)
        {
            var source = cmb.ItemsSource;
            if (source != null)
            {
                var newsource = (List<KeyValueComboBoxItem>)source;
                foreach (var item in newsource)
                {
                    if (item.Value.ToString() == value)
                    {
                        cmb.SelectedItem = item;
                        return;
                    }
                }
            }
        }
    }
}
