using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class DentistaDAO
    {
        public void Cadastrar(DentistaDTO dentista)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Dentista (Nome, Email, Senha) VALUES
						(@nome,@email,@senha)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", dentista.Nome);
            comando.Parameters.AddWithValue("@email", dentista.Email);
            comando.Parameters.AddWithValue("@senha", dentista.Senha);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public DentistaDTO Login(DentistaDTO dentista)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Dentista WHERE email = @email and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", dentista.Email);
            comando.Parameters.AddWithValue("@senha", dentista.Senha);

            var dataReader = comando.ExecuteReader();

            dentista = new DentistaDTO();

            while (dataReader.Read())
            {
                dentista.ID = int.Parse(dataReader["ID"].ToString());
                dentista.Nome = dataReader["Nome"].ToString();
                dentista.Email = dataReader["Email"].ToString();
                dentista.Senha = dataReader["Senha"].ToString();
            }
            conexao.Close();

            return dentista;
        }

        internal bool VerificarDentista(DentistaDTO dentista)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Dentista WHERE email = @email";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", dentista.Email);

            var dataReader = comando.ExecuteReader();

            var dentistas = new List<DentistaDTO>();

            while (dataReader.Read())
            {
                dentista = new DentistaDTO();
                dentista.ID = int.Parse(dataReader["ID"].ToString());
                dentista.Nome = dataReader["Nome"].ToString();
                dentista.Email = dataReader["Email"].ToString();
                dentista.Senha = dataReader["Senha"].ToString();

                dentistas.Add(dentista);
            }
            conexao.Close();

            return dentistas.Count > 0;
        }
    }
}
