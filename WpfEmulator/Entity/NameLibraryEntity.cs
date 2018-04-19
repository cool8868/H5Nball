using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.WpfEmulator.Entity
{
    public class NameLibraryEntity
    {
        public NameLibraryEntity()
        {
            Summary = new List<WpfSummaryEntity>();
            Summary.Add(new WpfSummaryEntity("Prefix", "姓"));
            Summary.Add(new WpfSummaryEntity("Suffix", "名"));
            Summary.Add(new WpfSummaryEntity("PlayerNames", "注册用的球员名"));
        }
        public List<WpfSummaryEntity> Summary { get; set; }

        public List<DicNameprefixEntity> Prefix
        {
            get;
            set;
        }

        public List<DicNamesuffixEntity> Suffix
        {
            get;
            set;
        }

        public List<TemplatePlayerName> PlayerNames { get; set; }

            
    }
}
