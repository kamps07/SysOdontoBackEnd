using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class AnamneseDAO
    {
        public void Cadastrar(AnamneseDTO anamnese)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Anamnese (DataAnamnese,  Resposta) VALUES
						(@data,@resposta)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@data", anamnese.Data);
            comando.Parameters.AddWithValue("@resposta", anamnese.Resposta);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
