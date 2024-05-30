using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class AgendamentoDAO
    {
        public void CadastrarAgendamento(CriarAgendamentoDTO agendamento, int idClinica)
        {
            var data = $"{agendamento.Ano}/{agendamento.Mes}/{agendamento.Dia}";

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Agendamento (Dentista, Paciente, DataDaConsulta, Horario, Observacao, Clinica, Servico) VALUES
						(@dentista,@paciente, @data, @horario, @observacoes, @clinica, @servico)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@dentista", agendamento.Dentista);
            comando.Parameters.AddWithValue("@paciente", agendamento.Paciente);
            comando.Parameters.AddWithValue("@data", data);
            comando.Parameters.AddWithValue("@horario", agendamento.Horario);
            comando.Parameters.AddWithValue("@observacoes", agendamento.Observacoes);
            comando.Parameters.AddWithValue("@servico", agendamento.Servico);
            comando.Parameters.AddWithValue("@clinica", idClinica);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public List<string> ListarHorariosDisponiveis(DateOnly data, int idUsuario)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"SELECT Horario FROM Agendamento WHERE DataDaConsulta = @data AND Dentista = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@data", data.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@id", idUsuario);

            var dataReader = comando.ExecuteReader();

            var horariosIndisponiveis = new List<TimeOnly>();

            while (dataReader.Read())
            {
                var horario = dataReader.GetTimeSpan(0);
                var horaIndisponivel = new TimeOnly(horario.Hours, horario.Minutes, horario.Seconds);
                horariosIndisponiveis.Add(horaIndisponivel);
            }

            var horariosDisponiveis = new List<string>();
            var horarioInicial = new TimeOnly(08, 00, 00);
            var horarioFinal = new TimeOnly(20, 00, 00);

            while (horarioInicial <= horarioFinal)
            {
                if (horariosIndisponiveis.Contains(horarioInicial) == false)
                {
                    horariosDisponiveis.Add(horarioInicial.ToString("HH:mm"));
                }
                horarioInicial = horarioInicial.AddMinutes(30);
            }
            conexao.Close();


            return horariosDisponiveis;
        }

        public bool VerificarAgendamento(CriarAgendamentoDTO agendamento, int idClinica)
        {
            var data = $"{agendamento.Ano}/{agendamento.Mes}/{agendamento.Dia}";

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Agendamento WHERE DataDaConsulta = @data AND Horario = @horario";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@data", data);
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

        public List<AgendamentoDTO> ListarAgendamentos(int clinica, DateTime data)
        {

            var conexao = ConnectionFactory.Build();
            conexao.Open();
            var query = @"SELECT
                        A.ID, A.DataDaConsulta, A.Horario, A.Observacao,
                        D.ID as ID_Dentista, D.Nome as Nome_Dentista,
                        P.ID as ID_Paciente, P.Nome as Nome_Paciente,
                        S.ID as ID_Servico, S.Nome as Nome_Servico, S.Duracao
                        FROM Agendamento A
                        INNER JOIN Usuario D
                        ON A.Dentista = D.ID
                        INNER JOIN Paciente P
                        ON A.Paciente = P.ID
                        INNER JOIN Servico S
                        ON A.Servico = S.ID
                            WHERE DataDaConsulta = @data";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@data", data.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@clinica", clinica);

            var dataReader = comando.ExecuteReader();

            var agendamentos = new List<AgendamentoDTO>();

            while (dataReader.Read())
            {
                var agendamento = new AgendamentoDTO();
                agendamento.Clinica = new ClinicaDTO();

                agendamento.ID = int.Parse(dataReader["ID"].ToString());
                agendamento.Dentista = new DentistaDTO();
                agendamento.Servico = new ServicoDTO();
                agendamento.Paciente = new PacienteDTO();

                var dataHora = DateTime.ParseExact(dataReader["DataDaConsulta"].ToString(), "dd/MM/yyyy HH:mm:ss", null);
                agendamento.DataDaConsulta = DateOnly.FromDateTime(dataHora);


                agendamento.Horario = dataReader["Horario"].ToString();
                agendamento.Observacoes = dataReader["Observacao"].ToString();

                agendamento.Dentista.ID = int.Parse(dataReader["ID_Dentista"].ToString());
                agendamento.Dentista.Nome = dataReader["Nome_Dentista"].ToString();


                agendamento.Paciente.ID = int.Parse(dataReader["ID_Paciente"].ToString());
                agendamento.Paciente.Nome = dataReader["Nome_Paciente"].ToString();

                agendamento.Servico.ID = int.Parse(dataReader["ID_Servico"].ToString());
                agendamento.Servico.Nome = dataReader["Nome_Servico"].ToString();
                agendamento.Servico.Duracao = int.Parse(dataReader["Duracao"].ToString());

                agendamentos.Add(agendamento);
            }

            conexao.Close();
            return agendamentos;
        }




    }
}
