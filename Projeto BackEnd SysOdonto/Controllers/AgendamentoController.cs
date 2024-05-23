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
        public IActionResult CadastrarAgendamento([FromBody] AgendamentoDTO agendamento)
        {
            var idClinica = int.Parse(HttpContext.User.FindFirst("Clinica")?.Value);
            var dao = new AgendamentoDAO();

            agendamento.Clinica = new ClinicaDTO() { ID = idClinica };

            bool agendamentoExiste = dao.VerificarAgendamento(agendamento);
            if (agendamentoExiste)
            {
                var mensagem = "Agendamento já existe na base de dados";
                return Conflict(mensagem);
            }


            dao.CadastrarAgendamento(agendamento);
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
