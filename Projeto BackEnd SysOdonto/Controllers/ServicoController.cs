using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {

            var clinicaID = int.Parse(HttpContext.User.FindFirst("Clinica")?.Value);

            var dao = new ServicoDAO();
            var servicos = dao.Listar(clinicaID);

            return Ok(servicos);
        }
    }
}
