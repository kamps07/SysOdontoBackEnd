using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;
using System;
using static Projeto_BackEnd_SysOdonto.DAOs.DocumentosDTO;
using static System.Net.Mime.MediaTypeNames;

namespace Projeto_BackEnd_SysOdonto.DAOs
{
    public class DocumentosDAO
    {
        public void Inserir(AdicionarDocumentosDTO request)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                foreach (var documento in request.Documentos)
                {

                    var query = @"INSERT INTO Documentos (Titulo, Descricao, Link, DataUpload, Paciente) VALUES (@titulo, @desc, @link, @data, @paciente)";

                    using (var comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@titulo", documento.Titulo);
                        comando.Parameters.AddWithValue("@desc", request.Descricao);
                        comando.Parameters.AddWithValue("@link", documento.Link);
                        comando.Parameters.AddWithValue("@data", DateTime.Now);
                        comando.Parameters.AddWithValue("@paciente", request.Paciente);

                        comando.ExecuteNonQuery();
                    }

                }
            }
        }

        public List<DocumentoDTO> ListarDocumentos(int paciente)
        {
            var documentos = new List<DocumentoDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();
                var query = "SELECT * FROM Documentos WHERE Paciente = @paciente";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@paciente", paciente);
                    using (var dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var documento = new DocumentoDTO
                            {
                                Id = Convert.ToInt32(dataReader["Id"]),
                                Titulo = dataReader["Titulo"].ToString(),
                                Descricao = dataReader["Descricao"].ToString(),
                                Link = dataReader["Link"].ToString(),
                                Paciente = Convert.ToInt32(dataReader["Paciente"]),
                                DataUpload = DateTime.ParseExact(dataReader["DataUpload"].ToString(), "dd/MM/yyyy HH:mm:ss", null)

                            };

                            documentos.Add(documento);
                        }
                    }
                }
            }

            return documentos;
        }
    }
}
