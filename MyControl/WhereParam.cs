using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Games.MyControl
{
    [TypeConverter(typeof(WhereParamItemConverter))]
    public class WhereParam
    {
        // Methods
        public WhereParam()
        {
        }

        public WhereParam(AdminConnect connectors, string fieldName, AdminCompare valueOperator, string defaultValue, string dateFormat, FieldType fieldType)
        {
            this.Connector = connectors;
            this.FieldName = fieldName;
            this.Operator = valueOperator;
            this.DefaultValue = defaultValue;
            this.DateFormat = dateFormat;
            this.FieldType = fieldType;
        }

        // Properties
        [Browsable(true), Description("条件名称"), NotifyParentProperty(true), Category("条件")]
        public string ConditionName { get; set; }

        [NotifyParentProperty(true), Description("连接符"), Category("条件")]
        public AdminConnect Connector { get; set; }

        [Browsable(false), Category("条件"), Description("日期格式"), NotifyParentProperty(true)]
        public string DateFormat { get; set; }

        [Category("条件"), Description("默认值(查询条件中需要过滤的值)"), NotifyParentProperty(true), Browsable(false)]
        public string DefaultValue { get; set; }

        [NotifyParentProperty(true), Category("条件"), Description("默认值(默认的查询输入值)")]
        public string FieldDefaultValue { get; set; }

        [Browsable(false), Category("条件"), NotifyParentProperty(true), Description("字段长度")]
        public int FieldLen { get; set; }

        [NotifyParentProperty(true), Description("字段名"), Category("条件")]
        public string FieldName { get; set; }

        private string _fieldNameN;
        [NotifyParentProperty(true), Description("字段名N"), Category("条件")]
        public string FieldNameN 
        { 
            get
            {
                if(string.IsNullOrEmpty(_fieldNameN))
                {
                    return FieldName;
                }
                else
                {
                    return _fieldNameN;
                }
            } 
            set { _fieldNameN = value; } 
        }

        [Description("数据类型"), NotifyParentProperty(true), Category("条件"), Browsable(false)]
        public FieldType FieldType { get; set; }

        [NotifyParentProperty(true), Description("字段对应的值"), Browsable(false), Category("条件")]
        public string FieldValue { get; set; }

        [Category("条件"), Description("值操作符"), NotifyParentProperty(true)]
        public AdminCompare Operator { get; set; }

        [NotifyParentProperty(true), Description("是否显示"), Browsable(false), Category("条件")]
        public bool IsDisable { get; set; }


    }
}
