using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.Azure;
using Projeto_BackEnd_SysOdonto.DTOs;
using Projeto_BackEnd_SysOdonto.DAOs;
using static Projeto_BackEnd_SysOdonto.DAOs.DocumentosDTO;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : Controller
    {
        [HttpPost]
        [Route("inserirdocumento")]
        public IActionResult InserirDocumento([FromBody] DocumentoDTO documento)
        {
            var documentosDAO = new DocumentosDAO();

            // Verificar se o documento já existe
            if (documentosDAO.VerificarDocumentoExistente(documento.Titulo))
            {
                return Conflict("Documento com o título já existe.");
            }

            // Upload do PDF para o Azure Blob Storage
            var azureBlobStorage = new AzureBlobStorage();
            documento.PDF = azureBlobStorage.UploadPdf(documento.Base64);

            // Inserir o documento no banco de dados
            documentosDAO.Inserir(documento);

            return Ok();
        }
    }
}
