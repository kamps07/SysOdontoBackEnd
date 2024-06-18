using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvolucaoController : Controller
    {
        [HttpPost]
        [Route("Adicionar")]
        public IActionResult AdicionarEvolucao([FromBody] EvolucaoDTO evolucao)
        {
            var dao = new EvolucaoDAO();

            if (evolucao.Status == "finalizado")
            {
                foreach (var tratamento in evolucao.Tratamento)
                {
                    dao.FinalizarTratamento(tratamento, evolucao.Paciente);
                }
            }

            dao.Adicionar(evolucao);

            return Ok();
        }



        [HttpGet]
        [Route("paciente/{paciente}")]
        public IActionResult Listar([FromRoute] int paciente)
        {
            var dao = new EvolucaoDAO();
            var evolucoes = dao.Listar(paciente);

            return Ok(evolucoes);
        }

    }
}
