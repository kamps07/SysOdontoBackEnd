using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class RecepcionistaDAO
    {
        public void Cadastrar(CadastroDTO recepcionista)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Recepcionista (Nome, Email, Senha) VALUES
						(@nome,@email,@senha)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", recepcionista.Nome);
            comando.Parameters.AddWithValue("@email", recepcionista.Email);
            comando.Parameters.AddWithValue("@senha", recepcionista.Senha);

            comando.ExecuteNonQuery();
            conexao.Close();
        }


        internal bool VerificarRecepcionista(CadastroDTO cadastro)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Recepcionista WHERE email = @email";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", cadastro.Email);

            var dataReader = comando.ExecuteReader();

            var recepcionistas = new List<RecepcionistaDTO>();

            while (dataReader.Read())
            {
                var recepcionista = new RecepcionistaDTO();
                recepcionista.ID = int.Parse(dataReader["ID"].ToString());
                recepcionista.Nome = dataReader["Nome"].ToString();
                recepcionista.Email = dataReader["Email"].ToString();
                recepcionista.Senha = dataReader["Senha"].ToString();

                recepcionistas.Add(recepcionista);
            }
            conexao.Close();

            return recepcionistas.Count > 0;
        }
    }
}
