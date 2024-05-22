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
        [Route("cadastrar")]
        [AllowAnonymous]
        public IActionResult CadastrarAnamnese([FromBody] AnamneseDTO anamnese)
        {

            var dao = new AnamneseDAO();
            dao.Cadastrar(anamnese);
            return Ok("Anamnese cadastrada com sucesso.");
        }
    }
}
