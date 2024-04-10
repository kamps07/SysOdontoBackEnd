using MySql.Data.MySqlClient;


    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            var connectionString = "Server=sysodontoetec.mysql.database.azure.com;Database=sysodonto;Uid=cadUsuario;Pwd=SysOdonto*;";
            return new MySqlConnection(connectionString);
        }
    }

