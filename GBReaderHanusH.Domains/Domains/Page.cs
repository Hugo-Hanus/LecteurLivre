using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Domains.Domains
{
    public class Page
    {
        public Page(string text,IList<Choice> choices)
        {
            Text = text;
            Choices = choices;
        }

        public string Text { get; set; }

        public IList<Choice> Choices { get; set; }

    }
}
