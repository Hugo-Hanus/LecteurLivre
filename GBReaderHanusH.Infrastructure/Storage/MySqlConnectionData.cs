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
        public string getDataBase() => "in19b1143";
        public string getPassword() => "5559";
        public string getProvider() => "MySql.Data.MySqlClient";
        public string getServer() => "192.168.128.13";
        public string getUserName() => "in19b1143";
        public DbProviderFactory getProviderFactory() => MySql.Data.MySqlClient.MySqlClientFactory.Instance;
    }
}
