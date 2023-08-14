using MySql.Data.MySqlClient;

namespace OtusPractice.WebApiCore.Services
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection Create()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
