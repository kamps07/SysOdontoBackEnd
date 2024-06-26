using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Projeto_BackEnd_SysOdonto.DTOs;
using static Projeto_BackEnd_SysOdonto.DAOs.DocumentosDTO;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class OdontogramaDAO
    {
        internal void Adicionar(OdontogramaUnitarioDTO odontograma)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"INSERT INTO Odontograma (Tratamento, Descricao, Dente,
                Paciente, Status, DataInicio, Posicao) 
                VALUES (@tratamento, @descricao, @dente, @paciente, @status, @data, @posicao)";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@tratamento", odontograma.Tratamento);
                    comando.Parameters.AddWithValue("@descricao", odontograma.Descricao);
                    comando.Parameters.AddWithValue("@paciente", odontograma.Paciente);
                    comando.Parameters.AddWithValue("@status", "Em andamento");
                    comando.Parameters.AddWithValue("@posicao", odontograma.Posicao);
                    comando.Parameters.AddWithValue("@dente", odontograma.Dente);
                    comando.Parameters.AddWithValue("@data", DateTime.Now);

                    comando.ExecuteNonQuery();
                }
            }
        }

        internal void Adicionar(OdontogramaMultiploDTO odontograma)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"INSERT INTO Odontograma (Tratamento, Descricao, Dente,
                Paciente, Status, DataInicio, Posicao) 
                VALUES (@tratamento, @descricao, @dente, @paciente, @status, @data, @posicao)";

                foreach (var dente in odontograma.Dentes)
                {
                    using (var comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@tratamento", odontograma.Tratamento);
                        comando.Parameters.AddWithValue("@descricao", odontograma.Descricao);
                        comando.Parameters.AddWithValue("@paciente", odontograma.Paciente);
                        comando.Parameters.AddWithValue("@status", "Em andamento");
                        comando.Parameters.AddWithValue("@posicao", odontograma.Posicao);
                        comando.Parameters.AddWithValue("@dente", dente);
                        comando.Parameters.AddWithValue("@data", DateTime.Now);

                        comando.ExecuteNonQuery();
                    }
                }
            }
        }

        internal List<OdontogramaUnitarioDTO> Listar(int paciente)
        {
            var odontogramas = new List<OdontogramaUnitarioDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();
                var query = "SELECT * FROM Odontograma WHERE Paciente = @paciente";

                using var comando = new MySqlCommand(query, conexao);

                comando.Parameters.AddWithValue("@paciente", paciente);

                using var dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    var odontograma = new OdontogramaUnitarioDTO();
                    odontograma.ID = Convert.ToInt32(dataReader["Id"]);
                    odontograma.Tratamento = dataReader["tratamento"].ToString();
                    odontograma.Descricao = dataReader["Descricao"].ToString();
                    odontograma.Paciente = int.Parse(dataReader["Paciente"].ToString());
                    odontograma.Status = dataReader["Status"].ToString();
                    odontograma.DataInicio = DateTime.ParseExact(dataReader["DataInicio"].ToString(), "dd/MM/yyyy HH:mm:ss", null);
                    odontograma.DataFim = string.IsNullOrWhiteSpace(dataReader["DataFim"].ToString()) ? null : DateTime.ParseExact(dataReader["DataFim"].ToString(), "dd/MM/yyyy HH:mm:ss", null);
                    odontograma.Dente = int.Parse(dataReader["Dente"].ToString());
                    odontograma.Posicao = dataReader["Posicao"].ToString();

                    odontogramas.Add(odontograma);
                }
            }

            return odontogramas;
        }
    }
}
