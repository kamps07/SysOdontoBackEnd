using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.Azure;
using Projeto_BackEnd_SysOdonto.DTOs;
using Projeto_BackEnd_SysOdonto.DAOs;
using System.Collections.Generic;
using static Projeto_BackEnd_SysOdonto.DAOs.DocumentosDTO;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly DocumentosDAO _documentosDAO;
        private readonly AzureBlobStorage _azureBlobStorage;

        public DocumentosController()
        {
            _documentosDAO = new DocumentosDAO();
            _azureBlobStorage = new AzureBlobStorage();
        }

        [HttpPost]
        [Route("InserirDocumento")]
        public IActionResult InserirDocumento([FromBody] DocumentoDTO documento)
        {
            // Verificar se o documento já existe
            if (_documentosDAO.VerificarDocumentoExistente(documento.Titulo))
            {
                return Conflict("Documento com o título já existe.");
            }

            // Upload do PDF para o Azure Blob Storage
            documento.PDF = _azureBlobStorage.UploadPdf(documento.Base64);

            // Inserir o documento no banco de dados
            _documentosDAO.Inserir(documento);

            return Ok("Documento inserido com sucesso.");
        }

        [HttpGet]
        [Route("ListarDocumentos")]
        public IActionResult ListarDocumentos()
        {
            var documentos = _documentosDAO.ListarDocumentos();

            if (documentos == null || documentos.Count == 0)
            {
                return NotFound("Nenhum documento encontrado na base de dados.");
            }

            return Ok(documentos);
        }
    }
}
