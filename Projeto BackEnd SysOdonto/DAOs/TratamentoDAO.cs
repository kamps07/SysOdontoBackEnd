using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;


namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class TratamentoDAO
    {
        public void CadastrarTratamento(TratamentoDTO tratamento )
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Tratamentos (Nome, Descricao) 
                        VALUES (@nome, @descricao)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", tratamento.Nome);
            comando.Parameters.AddWithValue("@dataNascimento", tratamento.Descricao);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

    }
}
