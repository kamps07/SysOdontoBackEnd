using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdontogramaController : ControllerBase
    {
        [HttpPost]
        [Route("Unitario")]
        public IActionResult CadastrarTratamentoUnitario([FromBody] OdontogramaUnitarioDTO odontograma)
        {
            var dao = new OdontogramaDAO();
            dao.Adicionar(odontograma);

            return Ok();
        }

        [HttpPost]
        [Route("Multiplo")]
        public IActionResult CadastrarTratamentoMultiplo([FromBody] OdontogramaMultiploDTO odontograma)
        {
            var dao = new OdontogramaDAO();
            dao.Adicionar(odontograma);

            return Ok();
        }

        [HttpGet]
        [Route("paciente/{paciente}")]
        public IActionResult Listar([FromRoute]int paciente)
        {
            var dao = new OdontogramaDAO();
            var odontogramas = dao.Listar(paciente);

            return Ok(odontogramas);
        }
    }
}
