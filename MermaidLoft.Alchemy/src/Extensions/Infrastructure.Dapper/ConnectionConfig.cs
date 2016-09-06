using System.Data;
using MySql.Data.MySqlClient;

namespace Infrastructure.Dapper
{
    public class ConnectionConfig
    {
        private static object _lockObject = new object();
        private static ConnectionConfig _instance = new ConnectionConfig();
        private ConnectionConfig()
        {
        }

        public static ConnectionConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConnectionConfig();
                        }
                    }
                }
                return _instance;
            }
        }

        private string _connectString = string.Empty;

        public void SetConnectString(string connectString)
        {
            _connectString = connectString;
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectString);
        }
    }
}
