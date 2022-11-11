using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Repository.Storage
{
    public interface ConnectionData
    {
        string getServer();
        string getUserName();
        string getPassword();
        string getDataBase();
        string getProvider();
        DbProviderFactory getProviderFactory();
    }
}
