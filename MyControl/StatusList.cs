using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Games.MyControl
{
    [TypeConverter(typeof(StatusListConverter))]
    public class StatusList
    {
        // Methods
        public StatusList()
        {
        }

        public StatusList(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }

        // Properties
        public string Text { get; set; }

        public string Value { get; set; }
    }
}
