using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Infrastructure.DTO
{
    public class DtoSession
    {
        public DtoSession(DateTime begin, DateTime lastUpdate,string title, int page)
        {
            Begin=begin;
            LastUpdate=lastUpdate;
            Page=page;
            Title = title;
        }

        public DateTime Begin { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Title { get; set; }
        public int Page { get; set; }

    }


}
