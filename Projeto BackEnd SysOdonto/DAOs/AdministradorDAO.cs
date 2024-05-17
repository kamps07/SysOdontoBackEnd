using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class AdministradorDAO
    {
        public void Cadastrar(AdministradorDTO administrador)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Administrador (Nome, Email, Senha, CPF) VALUES
						(@nome,@email,@senha, @cpf)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", administrador.Nome);
            comando.Parameters.AddWithValue("@email", administrador.Email);
            comando.Parameters.AddWithValue("@senha", administrador.Senha);
            comando.Parameters.AddWithValue("@cpf", administrador.CPF);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public AdministradorDTO Login(AdministradorDTO administrador)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Administrador WHERE email = @email and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", administrador.Email);
            comando.Parameters.AddWithValue("@senha", administrador.Senha);

            var dataReader = comando.ExecuteReader();

            var dao = new AdministradorDTO();

            while (dataReader.Read())
            {
                dao.ID = int.Parse(dataReader["ID"].ToString());
                dao.Email = dataReader["Email"].ToString();
                dao.Senha = dataReader["Senha"].ToString();
            }
            conexao.Close();

            return dao;
        }

        internal bool VerificarAdministrador(AdministradorDTO administrador)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Administrador WHERE CPF = @cpf";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@cpf", administrador.CPF);

            var dataReader = comando.ExecuteReader();

            var administradores= new List<AdministradorDTO>();

            while (dataReader.Read())
            {
                var dao = new AdministradorDTO();
                dao.ID = int.Parse(dataReader["ID"].ToString());
                dao.Nome = dataReader["Nome"].ToString();
                dao.Email = dataReader["Email"].ToString();
                dao.Senha = dataReader["Senha"].ToString();

                administradores.Add(dao);
            }
            conexao.Close();

            return administradores.Count > 0;
        }

    }
}
