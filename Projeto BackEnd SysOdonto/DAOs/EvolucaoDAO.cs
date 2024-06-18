using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Projeto_BackEnd_SysOdonto.DTOs;
using System.Collections.Generic;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class EvolucaoDAO
    {
        public void Adicionar(EvolucaoDTO evolucao)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"INSERT INTO Evolucao (Titulo, Descricao, Tratamento, DataEvolucao, Paciente) 
                          VALUES (@titulo, @descricao, @tratamento, @dataEvolucao, @paciente)";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@titulo", evolucao.Titulo);
                    comando.Parameters.AddWithValue("@descricao", evolucao.Descricao);
                    comando.Parameters.AddWithValue("@tratamento", string.Join(", ", evolucao.Tratamento));
                    comando.Parameters.AddWithValue("@dataEvolucao", DateTime.Now);
                    comando.Parameters.AddWithValue("@paciente", evolucao.Paciente);
                    comando.ExecuteNonQuery();
                }
            }
        }


        public void FinalizarTratamento(string tratamento, int paciente)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"UPDATE Odontograma SET Status = @status, DataFim = @dataFim 
                          WHERE Paciente = @paciente AND Tratamento = @tratamento";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@status", "Finalizado");
                    comando.Parameters.AddWithValue("@dataFim", DateTime.Now);
                    comando.Parameters.AddWithValue("@paciente", paciente);
                    comando.Parameters.AddWithValue("@tratamento", tratamento);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<EvolucaoDTO> Listar(int paciente)
        {
            var evolucoes = new List<EvolucaoDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();
                var query = "SELECT * FROM Evolucao WHERE Paciente = @paciente ORDER BY DataEvolucao DESC";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@paciente", paciente);

                    using (var dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var evolucao = new EvolucaoDTO();
                            evolucao.ID = Convert.ToInt32(dataReader["Id"]);
                            evolucao.Titulo = dataReader["Titulo"].ToString();
                            evolucao.Descricao = dataReader["Descricao"].ToString();
                            evolucao.Tratamento = dataReader["Tratamento"].ToString().Split(',').Select(t => t.Trim()).ToList();
                            evolucao.DataEvolucao = DateTime.ParseExact(dataReader["DataEvolucao"].ToString(), "dd/MM/yyyy HH:mm:ss", null);
                            evolucao.Paciente = int.Parse(dataReader["Paciente"].ToString());
                            evolucoes.Add(evolucao);
                        }
                    }
                }
            }

            return evolucoes;
        }
    }

}
