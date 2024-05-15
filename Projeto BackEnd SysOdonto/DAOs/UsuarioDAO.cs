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

            var query = @"INSERT INTO Usuario (Nome, Email, Senha, Funcao, Clinica) VALUES
						(@nome,@email,@senha, @funcao, @clinica)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            comando.Parameters.AddWithValue("@funcao", usuario.Funcao);
            comando.Parameters.AddWithValue("@clinica", usuario.Clinica.ID);

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

            var query = @"SELECT U.ID, U.Nome, Email, Senha, Funcao, 
                            C.ID as IDClinica, C.Nome as Clinica, ImgURL
                            FROM Usuario U
                            INNER JOIN Clinica C
                            ON U.Clinica = C.ID
                            WHERE email = @email and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            var dataReader = comando.ExecuteReader();

            var dto = new UsuarioDTO();

            while (dataReader.Read())
            {
                dto.ID = int.Parse(dataReader["ID"].ToString());
                dto.Email = dataReader["Email"].ToString();
                dto.Senha = dataReader["Senha"].ToString();
                dto.Funcao = dataReader["Funcao"].ToString();
                dto.Nome = dataReader["Nome"].ToString();
                dto.Clinica = new ClinicaDTO();
                dto.Clinica.ID = int.Parse(dataReader["IDClinica"].ToString());
                dto.Clinica.Nome = dataReader["Clinica"].ToString();
                dto.Clinica.ImgURL = dataReader["ImgURL"].ToString();
            }
            conexao.Close();

            return dto;
        }

        internal List<UsuarioDTO> ListarDentistas(int clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = "SELECT*FROM Usuario WHERE Funcao = 'Dentista' AND Clinica = @clinica";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@clinica", clinica);

            var dataReader = comando.ExecuteReader();

            var dentistas =  new List<UsuarioDTO>();

            if (dataReader.Read())
            {
                var dentista = new UsuarioDTO();
                dentista.ID = int.Parse(dataReader["ID"].ToString());
                dentista.Email = dataReader["Email"].ToString();
                dentista.Funcao = dataReader["Funcao"].ToString();
                dentista.Nome = dataReader["Nome"].ToString();
                dentistas.Add(dentista);
            }
            conexao.Close();

            return dentistas;
        }
    }
}
