
using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs; 
using static Projeto_BackEnd_SysOdonto.DAOs.DocumentosDTO;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class DocumentosDAO
    {
        public void Inserir(DocumentoDTO documento)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"INSERT INTO Documentos (Titulo, Conteudo, PDF) VALUES (@titulo, @conteudo,@pdf)";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@titulo", documento.Titulo);
                    comando.Parameters.AddWithValue("@conteudo", documento.Conteudo);
                    comando.Parameters.AddWithValue("@pdf", documento.PDF);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public bool VerificarDocumentoExistente(string titulo)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = "SELECT COUNT(*) FROM Documentos WHERE Titulo = @titulo";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@titulo", titulo);

                    int count = (int)comando.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}


