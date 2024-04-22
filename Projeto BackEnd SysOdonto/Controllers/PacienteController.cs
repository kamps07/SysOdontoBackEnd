using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        [HttpPost]
        [Route("cadastrarpaciente")]
        public IActionResult Cadastrarpaciente([FromBody] PacienteDTO paciente)
        {
            var dao = new PacienteDAO();

            bool PacienteExiste = dao.VerificarPaciente(paciente);
            if (PacienteExiste)
            {
                var mensagem = "Paciente já existe na base de dados";
                return Conflict(mensagem);
            }

            // Se o paciente não existe, cadastra e retorna Ok
            dao.CadastrarPaciente(paciente);
            return Ok();
        }

 

        [HttpGet]
        [Route("listarPacientes")]
        public IActionResult ListarPaciente(string CPF)
        {
            if (string.IsNullOrEmpty(CPF))
            {
                var mensagem = "CPF não fornecido.";
                return BadRequest(mensagem);
            }

            var dao = new PacienteDAO();
            var paciente = dao.ListarPaciente(CPF);

            if (paciente == null)
            {
                var mensagem = "Paciente não encontrado na base de dados.";
                return NotFound(mensagem);
            }

            return Ok(paciente);
        }

        //[HttpPut]
        //[Route("AtualizarMatricula")]
        //public IActionResult AtualizarMatricula(int matriculaId, string status)
        //{
        //    var dao = new AlunosDAO();
        //    dao.AtualizarMatricula(matriculaId, status);

        //    return Ok();
        //}


        [HttpPut("alterarPaciente")]
        public IActionResult AlterarPaciente([FromBody] PacienteDTO paciente)
        {
            if (paciente == null)
            {
                return BadRequest("Objeto Paciente não pode ser nulo");
            }

            var dao = new PacienteDAO();
            dao.AlterarPaciente(paciente);
            return Ok();
        }

    }
}
