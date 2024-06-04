using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.Azure;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;


namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : Controller
    {
        [HttpPost]
        [Route("cadastrarclinica")]
        public IActionResult Cadastrarclinica([FromBody] ClinicaDTO clinica)
        {
            var dao = new ClinicaDAO();

            bool clinicaExiste = dao.VerificarClinica(clinica);
            if (clinicaExiste)
            {
                var mensagem = "Clínica já existe na base de dados";
                return Conflict(mensagem);
            }

            var azureBlobStorage = new AzureBlobStorage();
            clinica.ImgURL = azureBlobStorage.UploadImage(clinica.Base64);

            dao.CadastrarClinica(clinica);
            return Ok();
        }
    }
}
