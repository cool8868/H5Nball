using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Games.MyControl
{
[TypeConverter(typeof(EditFieldParamConverter))]
public class EditFieldParam
{
    private List<StatusList> sltList;

    // Methods
    public EditFieldParam()
    {
    }

    public EditFieldParam(string fieldName, string fieldDescN, bool isEdit)
    {
        this.FieldName = fieldName;
        this.FieldDescN = fieldDescN;
        this.IsEdit = isEdit;
    }

    // Properties
    [NotifyParentProperty(true), Browsable(false), Category("字段"), Description("默认值")]
    public string DefaultValue { get; set; }

    [DefaultValue(""), NotifyParentProperty(true), Category("字段"), Description("字段说明")]
    public string FieldDescN { get; set; }

    [NotifyParentProperty(true), Browsable(false), Category("字段"), Description("字段长度")]
    public int FieldLen { get; set; }

    [NotifyParentProperty(true), DefaultValue(""), Description("字段名"), Category("字段")]
    public string FieldName { get; set; }

    [Browsable(false), Description("数据类型"), Category("字段"), NotifyParentProperty(true)]
    public FieldType FieldType { get; set; }

    [Description("是否允许编辑"), Category("字段"), NotifyParentProperty(true)]
    public bool IsEdit { get; set; }

    [Description("是否为文本域"), NotifyParentProperty(true), Category("字段")]
    public bool IsTextarea { get; set; }

    [NotifyParentProperty(true), Category("字段"), Description("是否更新时间默认值")]
    public bool IsTimeUpdate { get; set; }

    [Description("编辑行的显示类"), NotifyParentProperty(true), Category("字段")]
    public string RowClass { get; set; }

    [Description("下拉框信息"), PersistenceMode(PersistenceMode.InnerProperty), Editor(typeof(StatusListEditor), typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("字段"), NotifyParentProperty(true)]
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

    [NotifyParentProperty(true), Category("字段"), Description("值的描述")]
    public string ValueMemo { get; set; }
}
}
