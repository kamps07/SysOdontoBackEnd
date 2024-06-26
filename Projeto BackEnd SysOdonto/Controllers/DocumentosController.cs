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
        public IActionResult InserirDocumento([FromBody] AdicionarDocumentosDTO request)
        {
            foreach (var documento in request.Documentos)
            {
                documento.Link = _azureBlobStorage.UploadArquivo(documento);
            }

            _documentosDAO.Inserir(request);

            return Ok("Documento inserido com sucesso.");
        }



        [HttpGet]
        [Route("{paciente}")]
        public IActionResult ListarDocumentos([FromRoute] int paciente)
        {
            var documentos = _documentosDAO.ListarDocumentos(paciente);

            return Ok(documentos);
        }
    }
}
