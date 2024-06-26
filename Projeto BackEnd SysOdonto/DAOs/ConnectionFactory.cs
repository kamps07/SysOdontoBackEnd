using MySql.Data.MySqlClient;


    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
        var connectionString = "Server=sysodontoetec.mysql.database.azure.com;Database=sysodonto;Uid=cadUsuario;Pwd=SysOdonto*;";
        /* var connectionString = "Server=localhost;Database=sysodonto;Uid=root;Pwd=root;"; (SE PRECISAR)*/ 
        return new MySqlConnection(connectionString);
        }
    }

