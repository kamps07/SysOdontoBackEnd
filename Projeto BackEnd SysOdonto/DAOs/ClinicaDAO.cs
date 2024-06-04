using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class ClinicaDAO
    {
        public void CadastrarClinica(ClinicaDTO clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Clinica (Nome, Endereco, Telefone, ImgURL) VALUES
                (@nome, @endereco, @telefone, @imgurl)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", clinica.Nome);
            comando.Parameters.AddWithValue("@endereco", clinica.Endereco);
            comando.Parameters.AddWithValue("@telefone", (clinica.Telefone));
            comando.Parameters.AddWithValue("@imgurl", clinica.ImgURL);

            comando.ExecuteNonQuery();
            conexao.Close();
        }
        internal bool VerificarClinica(ClinicaDTO clinica)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Clinica WHERE Nome = @nome";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", clinica.Nome);

            var dataReader = comando.ExecuteReader();

            var clinicas = new List<ClinicaDTO>();

            while (dataReader.Read())
            {
                var dto = new ClinicaDTO();
                dto.ID = int.Parse(dataReader["ID"].ToString());
                dto.Nome = dataReader["Nome"].ToString();
                dto.Telefone = dataReader["Telefone"].ToString();
                dto.Endereco = dataReader["Endereco"].ToString();

                clinicas.Add(dto);
            }
            conexao.Close();

            return clinicas.Count > 0;
        }
    }
}
 