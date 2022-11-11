using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Repository.CustomException
{
        public class CantSelectException : Exception
        {
            public CantSelectException(string message) : base(message) { }
            public CantSelectException(Exception e, string message) : base(message, e) { }

        }
    
}
