using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.UI;

namespace Games.MyControl
{
    [TypeConverter(typeof(FieldParamItemConverter))]
    public class FieldParam
    {
        private List<StatusList> sltList;

        // Methods
        public FieldParam()
        {
        }

        public FieldParam(string fieldName, string fieldDescN, FieldType fieldType)
        {
            this.FieldName = fieldName;
            this.FieldDescN = fieldDescN;
            this.FieldType = fieldType;
        }

        // Properties
        [Description("字段内容长度"), NotifyParentProperty(true), Category("字段")]
        public int FieldContentLen { get; set; }

        [Category("字段"), DefaultValue(""), Description("字段说明"), NotifyParentProperty(true)]
        public string FieldDescN { get; set; }

        [Description("字段长度"), NotifyParentProperty(true), Browsable(false), Category("字段")]
        public int FieldLen { get; set; }

        [NotifyParentProperty(true), Category("字段"), DefaultValue(""), Description("字段名")]
        public string FieldName { get; set; }

        [Browsable(false), Category("字段"), Description("数据类型"), NotifyParentProperty(true)]
        public FieldType FieldType { get; set; }

        [Category("字段"), Description("是否可以编辑"), NotifyParentProperty(true)]
        public bool IsEdit { get; set; }

        [Category("字段"), Description("是否作为图片显示"), NotifyParentProperty(true)]
        public bool IsImg { get; set; }

        [NotifyParentProperty(true), DefaultValue(true), Description("是否输出列"), Browsable(false), Category("字段")]
        public bool IsOutput { get; set; }

        [Description("是否有排序功能"), Category("字段"), NotifyParentProperty(true), DefaultValue("")]
        public bool IsSort { get; set; }

        [Category("字段"), NotifyParentProperty(true), DefaultValue(""), Description("字段链接字符串，此字段链接到另一个页面当做条件查询")]
        public string Links { get; set; }

        [Category("字段"), DefaultValue("self"), Description("链接Target"), NotifyParentProperty(true)]
        public string LinkTarget { get; set; }

        [Category("字段"), Description("点击事件"), NotifyParentProperty(true)]
        public string OnClick { get; set; }

        [Category("字段"), NotifyParentProperty(true), Description("输出文本，为空显示字段本身的内容")]
        public string RenderText { get; set; }

        [DefaultValue("false"), Category("字段"), Description("是否提供超链接"), NotifyParentProperty(true)]
        public bool ShowLinks { get; set; }

        [Category("字段"), NotifyParentProperty(true), Description("输出文本，为空显示字段本身的内容")]
        public string EnumName { get; set; }

        [Category("字段"), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty), Description("下拉框信息"), Editor(typeof(StatusListEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<StatusList> SltList
        {
            get
            {
                if (this.sltList == null)
                {
                    this.sltList = new List<StatusList>();
                }
                return this.sltList;
            }
            set
            {
                this.sltList = value;
            }
        }

        [Category("字段"), Description("排序值"), NotifyParentProperty(true), Browsable(false)]
        public string SortValue { get; set; }

        [DefaultValue(""), Category("字段"), Description("列标头Title说明"), NotifyParentProperty(true)]
        public string Title { get; set; }
    }
}
