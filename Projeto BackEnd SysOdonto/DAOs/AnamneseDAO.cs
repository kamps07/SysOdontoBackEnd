using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class AnamneseDAO
    {
        public void CadastrarResposta(AnamneseDTO anamnese)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO resposta (DataAnamnese,  Resposta) VALUES
						(@data,@resposta)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@data", anamnese.Data);
            comando.Parameters.AddWithValue("@resposta", anamnese.Resposta);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void CadastrarPergunta(AnamneseDTO anamnese)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO pergunta (Valor) VALUES
						(@valor)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@valor", anamnese.Pergunta);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
