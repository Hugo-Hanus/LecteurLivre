using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Infrastructure.DTO
{
    public class DtoHistory
    {

        public DtoHistory(IDictionary<string, DtoSession> history)
        {
            DtoListSession = history;
        }

        public IDictionary<string, DtoSession> DtoListSession {get;set;}
    }
}
