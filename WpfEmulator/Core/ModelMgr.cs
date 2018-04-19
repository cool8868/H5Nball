using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Games.NBall.WpfEmulator.Core
{
    public class ModelMgr
    {
        private static readonly Dictionary<int, string> _mapping = new Dictionary<int, string>(10);

        static ModelMgr()
        {
            XDocument doc = XDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\SkillConfig\Models.xml");

            var elements = from item in doc.Descendants("Model")
                           select item;

            foreach (var item in elements)
            {
                _mapping.Add(Int32.Parse(item.Attribute("id").Value), item.Attribute("name").Value);
            }
        }

        public static string GetStr(string key)
        {
            return _mapping[int.Parse(key)];
        }
    }
}
