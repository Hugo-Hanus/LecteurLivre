using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBReaderHanusH.Repository.CustomException
{
    public class ConnectionFailedException : InvalidOperationException
    {

        public ConnectionFailedException(InvalidOperationException e, string message) : base(message,e) { }

    }
}
