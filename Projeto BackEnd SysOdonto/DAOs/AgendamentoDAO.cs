using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class AgendamentoDAO
    {
        public void NovoAgendamento(AgendamentoDTO agendamento)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Agendamento (Dentista, Paciente, Data, Horario, Duracao, Observacoes) VALUES
						(@dentista,@paciente, @data, @horario, @duracao, @observacoes)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@dentista", agendamento.Dentista);
            comando.Parameters.AddWithValue("@paciente", agendamento.Paciente);
            comando.Parameters.AddWithValue("@data", agendamento.Data);
            comando.Parameters.AddWithValue("@horario", agendamento.Horario);
            comando.Parameters.AddWithValue("@duracao", agendamento.Duracao);
            comando.Parameters.AddWithValue("@observacoes", agendamento.Observacoes);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
        public bool VerificarAgendamento(AgendamentoDTO agendamento)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Agendamento WHERE Data = @data AND Horario = @horario";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@data", agendamento.Data);
            comando.Parameters.AddWithValue("@horario", agendamento.Horario);

            var dataReader = comando.ExecuteReader();


            bool agendamentoEncontrado = false;


            if (dataReader.Read())
            {

                agendamentoEncontrado = true;
            }
            conexao.Close();


            return agendamentoEncontrado;
        }
    }
}
