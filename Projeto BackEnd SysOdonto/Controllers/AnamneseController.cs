using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnamneseController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarResposta")]
        [AllowAnonymous]
        public IActionResult CadastrarResposta([FromBody] AnamneseDTO anamnese)
        {

            var dao = new AnamneseDAO();
            dao.CadastrarResposta(anamnese);
            return Ok("Resposta cadastrada com sucesso.");
        }

        [HttpPost]
        [Route("CadastrarPergunta")]
        [AllowAnonymous]
        public IActionResult CadastrarPergunta([FromBody] AnamneseDTO anamnese)
        {

            var dao = new AnamneseDAO();
            dao.CadastrarPergunta(anamnese);
            return Ok("Pergunta cadastrada com sucesso.");
        }
    }
}
