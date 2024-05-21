
using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DTOs;
using System;
using System.Collections.Generic;
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

                var query = @"INSERT INTO Documentos (Titulo, Conteudo, PDF) VALUES (@titulo, @conteudo, @pdf)";

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

                    var count = (Int64)comando.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        public List<DocumentoDTO> ListarDocumentos()
        {
            var documentos = new List<DocumentoDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();
                var query = "SELECT * FROM Documentos";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    using (var dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var documento = new DocumentoDTO
                            {
                                Id = Convert.ToInt32(dataReader["Id"]),
                                Titulo = dataReader["Titulo"].ToString(),
                                Conteudo = dataReader["Conteudo"].ToString(),
                                PDF = dataReader["PDF"].ToString()
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




