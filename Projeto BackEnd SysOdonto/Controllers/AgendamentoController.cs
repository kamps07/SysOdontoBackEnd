using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult NovoAgendamento([FromBody] AgendamentoDTO agendamento)
        {
            var dao = new AgendamentoDAO();

            bool agendamentoExiste = dao.VerificarAgendamento(agendamento);
            if (agendamentoExiste)
            {
                var mensagem = "Administrador já existe na base de dados";
                return Conflict(mensagem);
            }

            // Se o paciente não existe, cadastra e retorna Ok
            dao.NovoAgendamento(agendamento);
            return Ok();
        }
    }
}
