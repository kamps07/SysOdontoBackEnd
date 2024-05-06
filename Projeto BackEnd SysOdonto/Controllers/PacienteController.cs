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
        [Route("CadastrarPaciente")]
        public IActionResult Cadastrarpaciente([FromBody] PacienteDTO paciente)
        {
            var dao = new PacienteDAO();

            if (!dao.EmailValido(paciente.Email))
            {
                var mensagem = "O e-mail fornecido é inválido.";
                return BadRequest(mensagem);
            }

            if (!dao.CPFValido(paciente.CPF))
            {
                var mensagem = "O CPF forncecido é inválido";
                return BadRequest(mensagem);
            }


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
        [Route("ListarPacientes")]
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

        [HttpPut("AlterarPaciente")]
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

        
        [HttpDelete]
        [Route("DeletarPacientes")]
        public IActionResult RemoverPaciente (PacienteDTO paciente)
        {
            var dao = new PacienteDAO();

            dao.RemoverPaciente(paciente.CPF);

            return Ok();
        }

    }
}
