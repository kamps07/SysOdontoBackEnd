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
            var idClinica = int.Parse(HttpContext.User.FindFirst("Clinica")?.Value);
            var idDentista = int.Parse(HttpContext.User.FindFirst("Dentista")?.Value);
            var idPaciente = int.Parse(HttpContext.User.FindFirst("Paciente")?.Value);


            var dao = new AgendamentoDAO();

            bool agendamentoExiste = dao.VerificarAgendamento(agendamento);
            if (agendamentoExiste)
            {
                var mensagem = "Agendamento já existe na base de dados";
                return Conflict(mensagem);
            }

            agendamento.Clinica = new ClinicaDTO() { ID = idClinica };
            agendamento.Dentista = new DentistaDTO() {  ID = idDentista };
            agendamento.Paciente = new PacienteDTO() {  ID = idPaciente };


            // Se o paciente não existe, cadastra e retorna Ok
            dao.NovoAgendamento(agendamento);
            return Ok();
        }
    }
}
