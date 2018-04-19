using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Games.MyControl
{
public class FieldParamItemConverter : ExpandableObjectConverter
{
    // Methods
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return ((destinationType == typeof(string)) || base.CanConvertTo(context, destinationType));
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value == null)
        {
            return new FieldParam();
        }
        if (!(value is string))
        {
            return base.ConvertFrom(context, culture, value);
        }
        string str = (string) value;
        if (str.Length == 0)
        {
            return new FieldParam();
        }
        return "FieldParam";
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if ((value != null) && !(value is FieldParam))
        {
            throw new ArgumentException("Invalid FieldParam", "value");
        }
        if (destinationType != typeof(string))
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }
        if (value == null)
        {
            return string.Empty;
        }
        return "FieldParam";
    }
}
}
