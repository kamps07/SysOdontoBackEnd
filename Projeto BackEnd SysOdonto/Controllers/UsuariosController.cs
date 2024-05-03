using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_BackEnd_SysOdonto.DAOs;
using Projeto_BackEnd_SysOdonto.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Projeto_BackEnd_SysOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public IActionResult Cadastrar([FromBody] CadastroDTO cadastro)
        {
            var daodentista = new DentistaDAO();
            var daorecepcionista = new RecepcionistaDAO();

            bool dentistaExiste = daodentista.VerificarDentista(cadastro);
            if (dentistaExiste)
            {
                var mensagem = "E-mail já existe na base de dados";
                return Conflict(mensagem);
            }

            bool recepcionistaExiste = daorecepcionista.VerificarRecepcionista(cadastro);
            if (recepcionistaExiste)
            {
                var mensagem = "E-mail já existe na base de dados";
                return Conflict(mensagem);
            }

            try
            {

                if (cadastro.Funcao == "Dentista")
                {
                    daodentista.Cadastrar(cadastro);
                }
                else if (cadastro.Funcao == "Recepcionista")
                {
                    daorecepcionista.Cadastrar(cadastro);
                }
                else
                {
                    return BadRequest("Função de usuário inválida.");
                }

                return Ok("Usuário cadastrado com sucesso.");
            }
            catch
            {
                return StatusCode(500, "Erro ao cadastrar usuário: ");
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromForm] CadastroDTO cadastro)
        {
            try
            {
                if (cadastro.Funcao == "Dentista")
                {
                    var daodentista = new DentistaDAO();
                    var dentistaLogado = daodentista.Login(cadastro);

                    if (dentistaLogado.ID == null)
                        return Unauthorized();

                    var tokenDentista = GenerateJwtTokenDentista(dentistaLogado);

                    return Ok(new { tokenDentista });
                }
                else if (cadastro.Funcao == "Recepcionista")
                {
                    var daorecepcionista = new RecepcionistaDAO();
                    var recepcionistaLogado = daorecepcionista.Login(cadastro);

                    if (recepcionistaLogado.ID == null)
                        return Unauthorized();

                    var tokenRecepcionista = GenerateJwtTokenRecepcionista(recepcionistaLogado);

                    return Ok(new { tokenRecepcionista });
                }
                else
                {
                    return BadRequest("Função de usuário inválida.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao fazer login de usuário: " + ex.Message);
            }
        }



        private string GenerateJwtTokenDentista(DentistaDTO dentista)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim("ID", dentista.ID.ToString()),
        new Claim("Email", dentista.Email),
    };        

            var token = new JwtSecurityToken(
                "APISysOdonto", // Nome da sua API
                "APISysOdonto", // Nome da sua API
                claims, // Lista de claims
                expires: DateTime.UtcNow.AddDays(1), // Tempo de expiração do Token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJwtTokenRecepcionista(RecepcionistaDTO recepcionista)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim("ID", recepcionista.ID.ToString()),
        new Claim("Email", recepcionista.Email),
    };

            var token = new JwtSecurityToken(
                "APISysOdonto", // Nome da sua API
                "APISysOdonto", // Nome da sua API
                claims, // Lista de claims
                expires: DateTime.UtcNow.AddDays(1), // Tempo de expiração do Token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
