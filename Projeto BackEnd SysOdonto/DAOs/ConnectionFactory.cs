using MySql.Data.MySqlClient;


    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            var connectionString = "Server=localhost;Database=sysodonto;Uid=root;Pwd=root;";
            return new MySqlConnection(connectionString);
        }
    }

