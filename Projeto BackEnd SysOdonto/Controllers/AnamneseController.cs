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
        public IActionResult CadastrarResposta([FromBody] RespostaAnamneseDTO anamnese)
        {
            
            var dao = new AnamneseDAO();
            dao.CadastrarResposta(anamnese);
            return Ok("Resposta cadastrada com sucesso.");
        }

        [HttpGet]
        [Route("Buscar/{paciente}")]
        public IActionResult BuscarAnamnse(int paciente)

        {
            var dao = new AnamneseDAO();
            var anamnese = dao.ListarAnamneses(paciente);

            return Ok(anamnese);
        }

            private readonly AnamneseDAO _anamneseDao;

            public AnamneseController()
            {
                _anamneseDao = new AnamneseDAO(); // Ou use injeção de dependência
            }

            [HttpGet]
            [Route("BuscarPergunta")]
            public IActionResult BuscarPergunta()
            {
                var perguntas = _anamneseDao.ListarPerguntas(); // Chame o método aqui
                return Ok(perguntas);
            }
    }
}
