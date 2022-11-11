using System;


namespace GBReaderHanusH.Repository.CustomException
{
    public class NoPathFindException: IOException
    {
        public NoPathFindException() { }

        public NoPathFindException(string message)
            : base(message) { }

        public NoPathFindException(string message, IOException inner)
            : base(message, inner) { }
    }
}
