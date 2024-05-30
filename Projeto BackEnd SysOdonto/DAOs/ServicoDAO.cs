using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class ServicoDAO
    {
        public List<ServicoDTO> Listar(int clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Servico WHERE Clinica = @clinica";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@clinica", clinica);

            var dataReader = comando.ExecuteReader();

            var servicos = new List<ServicoDTO>();

            while (dataReader.Read())
            {
                var servico = new ServicoDTO();
                servico.ID = int.Parse(dataReader["ID"].ToString());
                servico.Nome = dataReader["Nome"].ToString();
                servico.Duracao = int.Parse(dataReader["Duracao"].ToString());
                servico.ClinicaID = int.Parse(dataReader["Clinica"].ToString());

                servicos.Add(servico);
            }
            conexao.Close();

            return servicos;
        }
    }
}
