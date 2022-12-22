using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Infrastructure.DTO
{
    public class DtoPage
    {
        public DtoPage(string texte, IList<DtoChoice> choices)
        {
            Texte = texte;
            Choices = choices;
        }
        public string Texte { get; set; }
        public IList<DtoChoice> Choices { get; set; }
    }
}
