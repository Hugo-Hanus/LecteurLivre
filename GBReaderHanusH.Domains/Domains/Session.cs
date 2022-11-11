using System;

namespace GBReaderHanusH.Domains.Domains
{
    public class Session
    {
        public Session(DateTime begin, DateTime lastUpdate,string title, int page)
        {

            Begin = begin;
            LastUpdate = lastUpdate;
            Title = title;
            Page = page;
        }

        public DateTime Begin { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Title { get; set; }
        public int Page { get; set; }

        public string ToDetail() => $"{Title} | {Begin} | {LastUpdate}";
    }
}
