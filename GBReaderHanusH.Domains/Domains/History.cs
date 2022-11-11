using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Domains.Domains
{
    public class History
    {

        public History()
        {
            SessionList = new Dictionary<string, Session>();
        }

        public IDictionary<string, Session> SessionList
        {
            get; set;
        }

        public bool ContainsIsbn(string isbn)=> SessionList.ContainsKey(isbn);


        public int GetPageOfIsbn(string isbn) => SessionList[isbn].Page;

        public void AddSession(string isbn, Session newSession)
        {
            SessionList.Add(isbn, newSession);
        }
        public void UpdateReadingSession(string isbn,int page, DateTime dateTime)
        {
            SessionList[isbn].Page = page;
            SessionList[isbn].LastUpdate = dateTime;
        }

        public void ResetReadingSession(string isbn, DateTime dateTime)
        {
            SessionList[isbn].Begin = dateTime;
            SessionList[isbn].LastUpdate = dateTime;
        }
        public void DeleteReadingSession(string isbn)
        {
            SessionList.Remove(isbn);
        }

        public void setHistory(History history) => SessionList = history.SessionList;
    }
}
