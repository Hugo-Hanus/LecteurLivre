using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Repository.Storage
{
    public class MySqlConnectionData : ConnectionData
    {
        public string getDataBase() => "database";
        public string getPassword() => "password";
        public string getProvider() => "MySql.Data.MySqlClient";
        public string getServer() => "ipServer";
        public string getUserName() => "UserName";
        public DbProviderFactory getProviderFactory() => MySql.Data.MySqlClient.MySqlClientFactory.Instance;
    }
}
