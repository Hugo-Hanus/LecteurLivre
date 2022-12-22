using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Infrastructure.DTO
{
    public class DtoChoice
    {
        public DtoChoice(string texte, int goTo)
        {
            Texte = texte;
            GoTo = goTo;
        }
        public string Texte { get; set; }
        public int GoTo { get; set; }
    }
}
