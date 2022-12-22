using GBReaderHanusH.Domains.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Repository.Storage
{
    public interface ILibraryStorage
    {
      IList<string> SelectResumeGameBookPublish();
        IList<int> LoadIdPageOfGameBook(string isbn);
        int LoadNumberPage(int idPage);
        IList<string> SelectResumeGameBookSearch(string search);
         
    }
}
