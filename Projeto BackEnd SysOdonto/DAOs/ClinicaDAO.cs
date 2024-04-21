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

            var query = @"INSERT INTO Clinicas (Nome, Endereco, Telefone, ImagemUrl, Admintrador) VALUES
                (@nome, @endereco, @telefone, @imgurl, @administrador)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", clinica.Nome);
            comando.Parameters.AddWithValue("@endereco", clinica.Endereco);
            comando.Parameters.AddWithValue("@telefone", clinica.Telefone);
            comando.Parameters.AddWithValue("@imgurl", clinica.ImgURL);
            comando.Parameters.AddWithValue("@administrador", clinica.Administrador?.ID); // Assuming Administrador is an object with an ID property

            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
