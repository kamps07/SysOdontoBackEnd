using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class UsuarioDAO
    {
        public void Cadastrar(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Usuario (Nome, Email, Senha,  Funcao) VALUES
						(@nome,@email,@senha, @funcao)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            comando.Parameters.AddWithValue("@funcao", usuario.Funcao);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
        public bool VerificarUsuario(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Usuario WHERE Email = @email";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@email", usuario.Email);

            var dataReader = comando.ExecuteReader();


            bool usuarioEncontrado = false;


            if (dataReader.Read())
            {

                usuarioEncontrado = true;
            }
            conexao.Close();


            return usuarioEncontrado;
        }

        public UsuarioDTO Login(UsuarioDTO usuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Usuario WHERE email = @email and senha = @senha and Funcao = @funcao";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            comando.Parameters.AddWithValue("@funcao", usuario.Funcao);


            var dataReader = comando.ExecuteReader();

            var dao = new UsuarioDTO();

            while (dataReader.Read())
            {
                dao.ID = int.Parse(dataReader["ID"].ToString());
                dao.Email = dataReader["Email"].ToString();
                dao.Senha = dataReader["Senha"].ToString();
                dao.Funcao = dataReader["Funcao"].ToString();
            }
            conexao.Close();

            return dao;
        }
    }
}
