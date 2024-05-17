using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarAdministrador")]
        [AllowAnonymous]
        public IActionResult Cadastrar([FromBody] AdministradorDTO administrador)
        {
            var dao = new AdministradorDAO();

            bool admExiste = dao.VerificarAdministrador(administrador);
            if (admExiste)
            {
                var mensagem = "Administrador já existe na base de dados";
                return Conflict(mensagem);
            }
            dao.Cadastrar(administrador);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromForm] AdministradorDTO administrador)
        {
            var dao = new AdministradorDAO();
            var admLogado = dao.Login(administrador);

            if (admLogado == null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(admLogado);

            return Ok(new { token });
        }


        private string GenerateJwtToken(AdministradorDTO administrador)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim("ID", administrador.ID.ToString()),
                    new Claim("Email", administrador.Email),
                };

            var token = new JwtSecurityToken(
                "APISysOdonto", //Nome da sua api
                "APISysOdonto", //Nome da sua api
                claims, //Lista de claims
                expires: DateTime.UtcNow.AddDays(1), //Tempo de expiração do Token, nesse caso o Token expira em um dia
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
