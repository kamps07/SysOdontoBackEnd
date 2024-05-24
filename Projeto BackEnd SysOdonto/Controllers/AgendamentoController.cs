using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgendamentoController : ControllerBase
    {
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult CadastrarAgendamento([FromBody] CriarAgendamentoDTO agendamento)
        {
            var idClinica = int.Parse(HttpContext.User.FindFirst("Clinica")?.Value);

            var dao = new AgendamentoDAO();

            bool agendamentoExiste = dao.VerificarAgendamento(agendamento, idClinica);
            if (agendamentoExiste)
            {
                var mensagem = "Agendamento já existe na base de dados";
                return Conflict(mensagem);
            }

            dao.CadastrarAgendamento(agendamento, idClinica);
            return Ok();
        }

        [HttpGet]
        [Route("ListarHorariosDisponiveis")]
        
        public IActionResult ListarHorariosDisponiveis(int dia, int mes, int ano)
        {
            var data = new DateOnly(ano, mes, dia);
            var idUsuario = int.Parse(HttpContext.User.FindFirst("id")?.Value);
            var dao = new AgendamentoDAO();
            var horariosDisponiveis = dao.ListarHorariosDisponiveis(data, idUsuario);

            return Ok(horariosDisponiveis);
        }
    }
}
