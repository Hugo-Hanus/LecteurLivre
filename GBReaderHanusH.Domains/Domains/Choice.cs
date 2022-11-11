using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Domains.Domains
{
    public class Choice
    {
        public Choice(string text,int goTo)
        {
            Text = text;
            GoTo= goTo;
        }
        public string Text { get; set; }
        public int GoTo { get; set; }
    }
}
