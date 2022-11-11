using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace GBReaderHanusH.Repository.Storage
{
    public class StorageFactory
    {

        private readonly DbProviderFactory _factory;
        private readonly ConnectionData _connectionData;

        public StorageFactory(ConnectionData connectionData)
        {
            this._connectionData = connectionData;
            try
            {
                DbProviderFactories.RegisterFactory(connectionData.getProvider(), connectionData.getProviderFactory());
                _factory = DbProviderFactories.GetFactory(connectionData.getProvider());
            }
            catch(ArgumentException ex)
            {
                throw new ProviderNotFoundException($"Unable to load prodiver ${connectionData.getProviderFactory()}",ex);
            }
        }
        public LibraryStorage NewStorage()
        {
            try
            {
                IDbConnection con = _factory.CreateConnection()?? throw new ArgumentNullException(nameof(_factory));
                con.ConnectionString =$"Server={_connectionData.getServer()};User ID={_connectionData.getUserName()};Password={_connectionData.getPassword()};Database={_connectionData.getDataBase()}";
                con.Open();
                return new LibraryStorage(con);
            }
            catch (MySqlException ex)
            {
                throw new UnableToConnectException(ex);
            }
        }

        public class UnableToConnectException : Exception
        {
            public UnableToConnectException(Exception sqlException)
                : base("Impossible de se connecter à la base de donnée", sqlException)
            { }
        }

        public class ProviderNotFoundException : Exception
        {
            public ProviderNotFoundException(string s, Exception argumentException)
                : base(s, argumentException)
            {
            }
        }

        public class InvalidConnectionStringException : Exception
        {
            public InvalidConnectionStringException(Exception argumentException)
                : base("Unable to use this connection string", argumentException)
            {
            }
        }


    }
}
