using System;
using System.Data.SqlClient;
using NordicStore.Shared;
using System.Data;

namespace NordicStore.Infra.DataContexts
{
    public class NordicStoreDataContext : IDisposable
    {
        public SqlConnection Connection { get; private set; }

        public NordicStoreDataContext(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
