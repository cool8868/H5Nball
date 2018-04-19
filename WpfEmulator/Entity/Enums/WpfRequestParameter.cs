using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WpfEmulator.Entity
{
    public class WpfRequestParameter
    {
        private string _parameters = "";
        public WpfRequestParameter()
        {

        }

        public void AddHasTask(bool value)
        {
            Add("ht", value ? 1 : 0);
        }

        public void Add(string name, bool value)
        {
            Add(name, value ? 1 : 0);
        }

        public void Add(string name, int value)
        {
            Add(name, value.ToString());
        }

        public void Add(string name, Guid value)
        {
            Add(name, value.ToString());
        }

        public void Add(string name, string value)
        {
            _parameters += name + "=" + value + "&";
        }

        public override string ToString()
        {
            return _parameters.TrimEnd('&');
        }
    }
}
